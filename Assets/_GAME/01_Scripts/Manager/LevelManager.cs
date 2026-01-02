
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

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        EnemySpawner.Instance.Init(levelConfig);
    }

    public void InitializeEcomomyToPlayer()
    {
        EconomyManager.Instance.Init();
        
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
    }
    
    

    public void CheckCompleteLevel(Action onComplete = null)
    {
        
    }

    public void NextLevel(Action onComplete = null)
    {
        
    }
}
