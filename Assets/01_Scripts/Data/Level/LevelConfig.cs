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
        public int LevelID { get => levelID; set => levelID = value; }

        public List<WaveEnemyConfig> EnemyWave
        {
            get => enemyWave;
            set => enemyWave = value;
        }
        
        public int TotalWave => enemyWave.Count;
    }


    public class WaveEnemyConfig
    {
        [SerializeField] private List<GroupEnemy> groupEnemies;
        [SerializeField] private int timeBetweenWaves;
        public int TimeBetweenWaves { get => timeBetweenWaves; set => timeBetweenWaves = value; }
    }

    public class GroupEnemy
    {
        [SerializeField] private int total;
        [SerializeField] private EnemyConfig enemyConfig;
        [SerializeField] private int timeDelays;
        
        public int Total { get => total; set => total = value; }
        public int TimeDelay { get => timeDelays; set => timeDelays = value; }


    }
}