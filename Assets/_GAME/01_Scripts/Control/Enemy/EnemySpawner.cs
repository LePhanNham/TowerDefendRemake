using System.Collections;
using _01_Scripts.Data.Level;
using UnityEngine;
using UnityEngine.Serialization;


public class EnemySpawner : SingletonMono<EnemySpawner>
{
    [SerializeField] private WayPoint currentWayPoint;
    [FormerlySerializedAs("OnWaveCompleted")] [SerializeField] private IntEventControl onWaveCompleted;
    private LevelConfig currentLevelConfig;
    private int currentWaveIndex;
    private int maxWaveIndex;
    private bool isSpawning = false;
    public int MaxWaveIndex => maxWaveIndex;

    public void Init(LevelConfig levelConfig)
    {
        currentLevelConfig = levelConfig;
        currentWaveIndex = 0;
        maxWaveIndex = levelConfig.TotalWave;
        currentWayPoint = LevelManager.Instance.WayPoint;
        LevelManager.Instance.InitializeEcomomyToPlayer(levelConfig);
    }

    
    public void SpawnLevel()
    {
        if (currentWaveIndex >= maxWaveIndex)
        {
            return;
        }
        
        StartCoroutine(SpawnWaveEnemy(currentLevelConfig.EnemyWave[currentWaveIndex]));
        currentWaveIndex++;
    }

    IEnumerator SpawnWaveEnemy(WaveEnemyConfig waveEnemyConfig)
    {
        string curWave = currentWaveIndex < maxWaveIndex ? "Wave " + (currentWaveIndex + 1).ToString() : "Last Wave";
        GameEventManager.NotifyCurrentWave(curWave);
        LevelManager.Instance.Init(waveEnemyConfig);
        isSpawning = true;
        foreach (var groupEnemy in waveEnemyConfig.GroupEnemies)
        {
            StartCoroutine(SpawnGroupEnemy(groupEnemy));
            yield return new WaitForSeconds(waveEnemyConfig.TimeBetweenWaves);
        }
        isSpawning = false;
        yield return new WaitUntil(() => !isSpawning && EnemyManager.Instance.EnemyCountInWave() == 0);
        CompletedWave();
    }

    IEnumerator SpawnGroupEnemy(GroupEnemy enemy)
    {
        for (int i = 0; i < enemy.Total; i++)
        {
            var poolObj = PoolManager.Instance.Spawn(enemy.EnemyConfig.EnemyName, null);
            poolObj.transform.position = currentWayPoint.GetWaypointPosition(0);
            var enemyBase = poolObj.GetComponent<EnemyBase>();
            enemyBase.SetUpWayPoint(currentWayPoint);
            yield return new WaitForSeconds(enemy.TimeDelay);
        }

    }

    
    public void CompletedWave()
    {
        onWaveCompleted.Raise(currentWaveIndex);
    }
}
