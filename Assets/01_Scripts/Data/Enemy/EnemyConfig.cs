
using UnityEngine;

namespace _01_Scripts.Data.Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private string enemyName;
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private int hp;
        [SerializeField] private int atk;
        [SerializeField] private int def;
        [SerializeField] private int speed;

        public string EnemyName
        {
            get => enemyName;
            set => enemyName = value;
        }

        public EnemyType EnemyType
        {
            get => enemyType;
            set => enemyType = value;
        }
        public int Hp
        {
            get => hp;
            set => hp = value;
        }

        public int Atk
        {
            get => atk;
            set => atk = value;
        }

        public int Def
        {
            get => def;
            set => def = value;
        }

        public int Speed
        {
            get => speed;
            set => speed = value;
        }
    }
}
