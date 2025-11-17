
using _01_Scripts.Data.Level;
using UnityEngine;

public class LevelManager : SingletonMono<LevelManager>
{
    [Header("Event Control")] 
    [SerializeField] private IntEventControl OnUpdatedEnemies;
    private int totalEnemies;
    private int currentDeadEnemies;


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
