using System;
using System.Collections.Generic;
using _01_Scripts.Data.Enemy;
using UnityEngine;

namespace _01_Scripts.Data.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObject/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int levelID;
        [SerializeField] private List<WaveEnemyConfig> enemyWave;
        public int LevelID { get => levelID; }

        public List<WaveEnemyConfig> EnemyWave
        {
            get => enemyWave;
        }
        
        public int TotalWave => EnemyWave.Count;
    }

    [Serializable]
    public class WaveEnemyConfig
    {
        [SerializeField] private List<GroupEnemy> groupEnemies;
        [SerializeField] private int timeBetweenWaves;
        public List<GroupEnemy>  GroupEnemies { get => groupEnemies; }
        public int TimeBetweenWaves { get => timeBetweenWaves; }

        public int GetTotalEnemies()
        {
            int count = 0;
            foreach (var groupEnemy in groupEnemies)
            {
                count += groupEnemy.Total;
            }
            return count;
        }
    }
    [Serializable]
    public class GroupEnemy
    {
        [SerializeField] private int total;
        [SerializeField] private EnemyConfig enemyConfig;
        [SerializeField] private int timeDelays;

        public EnemyConfig EnemyConfig
        {
            get => enemyConfig;
            set => enemyConfig = value;
        }

        public int Total { get => total;  }
        public int TimeDelay
        {
            get => timeDelays;
        }


    }
}