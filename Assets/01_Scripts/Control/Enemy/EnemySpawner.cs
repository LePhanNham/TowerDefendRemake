using System.Collections;
using _01_Scripts.Data.Level;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : SingletonMono<EnemySpawner>
{
    [SerializeField] private WayPoint currentWayPoint;
    [SerializeField] private IntEventControl OnWaveCompleted;
    private LevelConfig currentLevelConfig;
    private int currentWaveIndex;
    private int maxWaveIndex;
    

    public void Init(LevelConfig levelConfig)
    {
        currentLevelConfig = levelConfig;
        currentWaveIndex = 0;
        maxWaveIndex = levelConfig.TotalWave;
    }

    
    public void SpawnLevel()
    {
        if (currentWaveIndex+1 >= maxWaveIndex)
        {
            return;
        }
        currentWaveIndex++;
        StartCoroutine(SpawnWaveEnemy(currentLevelConfig.EnemyWave[currentWaveIndex]));
        
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
            PoolManager.Instance.Spawn(enemy.EnemyConfig.EnemyName, null);
            yield return new WaitForSeconds(enemy.TimeDelay);
        }
    }

    public void CompletedWave()
    {
        OnWaveCompleted.Raise(currentWaveIndex);
    }
}
