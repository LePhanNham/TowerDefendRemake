
using System;
using _01_Scripts.Data.Level;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : SingletonMono<LevelManager>
{
    [FormerlySerializedAs("OnUpdatedEnemies")]
    [Header("Event Control")] 
    [SerializeField] private IntEventControl onUpdatedEnemies;

    [SerializeField] private WayPoint wayPoint;
    [SerializeField] private LevelConfig levelConfig;
    [Header("Base")]
    [SerializeField] private int baseMaxHp = 10;
    private int currentBaseHp;
    private int totalEnemies;
    private int currentDeadEnemies;

    public WayPoint WayPoint => wayPoint;
    public LevelConfig LevelConfig => levelConfig;
    public int BaseMaxHp => baseMaxHp;

    public void SetWayPoint(WayPoint wp)
    {
        wayPoint = wp;
    }

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        
        
    }

    [Obsolete]
    public void StartLevel()
    {
        // Reset any previous run state (turrets, enemies, economy, counters)
        ResetGameState();

        // set waypoint and initialize spawner/economy first so UI reflects correct state
        wayPoint = levelConfig.WayPoint;
        EnemySpawner.Instance.Init(levelConfig);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.OpenUI<CanvasGamePlay>();
        }

        GameManager.ChangeState(GameState.GamePlay);

        // Initialize base HP from level config (fallback to serialized value)
        if (levelConfig != null && levelConfig.BaseHp > 0)
            baseMaxHp = levelConfig.BaseHp;
        currentBaseHp = baseMaxHp;
        GameEventManager.BaseHpUpdated(currentBaseHp);

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.BeginTutorial();
        }
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMusic(SoundManager.SoundId.Music);
        }
    }

    [Obsolete]
    private void ResetGameState()
    {
        // Destroy all placed turrets and notify nodes
        var turrets = FindObjectsOfType<TurretBase>();
        foreach (var t in turrets)
        {
            try
            {
                var host = t.HostNode;
                if (host != null)
                {
                    GameEventManager.TurretSold(host);
                }
            }
            catch { }
            Destroy(t.gameObject);
        }

        // Despawn all active enemies and clear EnemyManager list
        if (EnemyManager.Instance != null)
        {
            var enemies = FindObjectsOfType<EnemyBase>();
            foreach (var e in enemies)
            {
                try
                {
                    var poolable = e.GetComponent<PoolableObject>();
                    if (poolable != null && e.EnemyConfig != null)
                    {
                        PoolManager.Instance?.Despawn(e.EnemyConfig.EnemyName, poolable);
                        PoolManager.Instance?.ReleaseToPool(e.EnemyConfig.EnemyName, poolable);
                    }
                }
                catch { }
            }
            EnemyManager.Instance.activeEnemies.Clear();
        }

        totalEnemies = 0;
        currentDeadEnemies = 0;

        if (EconomyManager.Instance != null && levelConfig != null)
        {
            EconomyManager.Instance.Init(levelConfig);
        }
    }
    private void OnEnable()
    {
        GameEventManager.onUpdatedEnemiesDie += UpdateEnemiesDead;
        
    }

    private void OnDisable()
    {
        GameEventManager.onUpdatedEnemiesDie -= UpdateEnemiesDead;
    }

    public void InitializeEcomomyToPlayer(LevelConfig config)
    {
        EconomyManager.Instance.Init(config);
        
    }   
    public void Init(WaveEnemyConfig waveEnemyConfig)
    {
        totalEnemies = waveEnemyConfig.GetTotalEnemies();

    }
    public void DamageBase(int damage = 1)
    {
        currentBaseHp -= damage;
        if (currentBaseHp < 0) currentBaseHp = 0;
        GameEventManager.BaseHpUpdated(currentBaseHp);
        if (SoundManager.Instance != null) SoundManager.Instance.Play(SoundManager.SoundId.BaseHit);
        if (currentBaseHp <= 0)
        {
            GameEventManager.BaseHpZero();
            GameManager.ChangeState(GameState.Finish);
        }
    }

    public int CurrentBaseHp => currentBaseHp;
    public void UpdateEnemiesDead()
    {
        currentDeadEnemies++;
        if (currentDeadEnemies == totalEnemies)
        {
            onUpdatedEnemies.Raise(currentDeadEnemies);
        }

        if (currentDeadEnemies == 1 && !TutorialManager.Instance.IsTutorialFinished)
        {
            TutorialManager.Instance.ReportAction(TutorialActionType.EnemyKilled);
        }
    }
    
    

    public void CompleteLevel(Action onComplete = null)
    {
        
    }

    public void NextLevel(Action onComplete = null)
    {
        StartLevel();
        onComplete?.Invoke();
    }
}
