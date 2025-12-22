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
    

    public void Init(LevelConfig levelConfig)
    {
        currentLevelConfig = levelConfig;
        currentWaveIndex = 0;
        maxWaveIndex = levelConfig.TotalWave;
        currentWayPoint = LevelManager.Instance.WayPoint;
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
        foreach (var groupEnemy in waveEnemyConfig.GroupEnemies)
        {
            StartCoroutine(SpawnGroupEnemy(groupEnemy));
            yield return new WaitForSeconds(waveEnemyConfig.TimeBetweenWaves);
        }
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
