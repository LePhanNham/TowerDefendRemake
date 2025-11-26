using _01_Scripts.Data.Enemy;
using CONSTANT.FSMSystem;
using UnityEngine;


public abstract class EnemyBase : FSMSystem
{
    [Header("Base Enemy Stats")] 
    [SerializeField] protected WayPoint currentWayPoint;
    
    [SerializeField] protected EnemyConfig enemyConfig;
    protected PoolableObject poolableObject;
    protected HealthControl hpControl;
    protected bool isDead = false;
    protected Vector3 pos;
    public int currentWayPointIndex;
    public HealthControl HPControl
    {
        get { return hpControl; }
        set { hpControl = value; }
    }
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public void SetUpWayPoint(WayPoint wp)
    {
        currentWayPoint = wp;
        currentWayPointIndex = 0;
        isDead = false;
    }

    protected virtual void Awake()
    {
        hpControl = GetComponent<HealthControl>();
        poolableObject = GetComponent<PoolableObject>();
    }
    protected override void Update()
    {
        if (!enabled) return;
        OnMove();
    }


    protected abstract void OnMove();

    protected abstract void OnDie();

    protected abstract void OnHit();
    

    public virtual void Die()
    {
        if (isDead) return;

        isDead = true;
        OnDie();
        // pool
    }

    public void ReachEndPoint()
    {
        PoolManager.Instance.ReleaseToPool(enemyConfig.EnemyName,poolableObject);
        PoolManager.Instance.Despawn(enemyConfig.EnemyName,poolableObject);
    }
}


public enum EnemyType
{
    Bat, Bird, Cattle, Dog, Feline, Giant, Insect, Scorpion
}

