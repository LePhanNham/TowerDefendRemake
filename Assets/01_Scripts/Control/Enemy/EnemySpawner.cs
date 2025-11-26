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
        currentWayPoint = LevelManager.Instance.WayPoint;
        Debug.Log(currentWaveIndex);
        Debug.Log(maxWaveIndex);
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
            var enemyprefab = PoolManager.Instance.Spawn(enemy.EnemyConfig.EnemyName, null);
            enemyprefab.gameObject.SetActive(true);
            enemyprefab.GetComponent<EnemyBase>().SetUpWayPoint(currentWayPoint);
            enemyprefab.transform.position = currentWayPoint.GetWaypointPosition(0);
            yield return new WaitForSeconds(enemy.TimeDelay);
        }
        
    }
    
    public void CompletedWave()
    {
        OnWaveCompleted.Raise(currentWaveIndex);
    }
}
