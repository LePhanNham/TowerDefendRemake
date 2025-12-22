
using _01_Scripts.Data.Level;
using UnityEngine;

public class LevelManager : SingletonMono<LevelManager>
{
    [Header("Event Control")] 
    [SerializeField] private IntEventControl OnUpdatedEnemies;

    [SerializeField] private WayPoint wayPoint;
    [SerializeField] private LevelConfig levelConfig;
    private int totalEnemies;
    private int currentDeadEnemies;

    public WayPoint WayPoint => wayPoint;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        EnemySpawner.Instance.Init(levelConfig);
        EnemySpawner.Instance.SpawnLevel();
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
            OnUpdatedEnemies.Raise(currentDeadEnemies);
        }
    }
    
    
}
