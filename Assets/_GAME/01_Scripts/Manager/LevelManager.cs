
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
    private int totalEnemies;
    private int currentDeadEnemies;

    public WayPoint WayPoint => wayPoint;
    public LevelConfig LevelConfig => levelConfig;

    // Allow runtime assignment when loading a map prefab that contains the WayPoint
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
    public void StartLevel()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.OpenUI<CanvasGamePlay>();
        }

        GameManager.ChangeState(GameState.GamePlay);
        wayPoint = levelConfig.WayPoint;
        EnemySpawner.Instance.Init(levelConfig);

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.BeginTutorial();
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
        
    }
}
