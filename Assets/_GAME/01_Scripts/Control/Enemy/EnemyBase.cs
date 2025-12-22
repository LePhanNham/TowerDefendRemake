using _01_Scripts.Data.Enemy;
using CONSTANT.FSMSystem;
using UnityEngine;


public abstract class EnemyBase : FSMSystem
{
    [Header("Base Enemy Stats")] 
    [SerializeField] protected WayPoint currentWayPoint;
    
    [SerializeField] protected EnemyConfig enemyConfig;
    protected PoolableObject PoolableObject;
    protected HealthControl HpControl;
    protected bool isDead;
    protected bool isAlive;
    public bool IsAlive => isAlive;
    protected Vector3 Pos;
    public int currentWayPointIndex;
    public HealthControl HPControl
    {
        get { return HpControl; }
        set { HpControl = value; }
    }
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    public void SetUpWayPoint(WayPoint wp)
    {
        currentWayPoint = wp;
    }

    protected virtual void Awake()
    {
        HpControl = GetComponent<HealthControl>();
        PoolableObject = GetComponent<PoolableObject>();
    }

    protected virtual void OnEnable()
    {
        isDead = false;
        isAlive = true;
        currentWayPointIndex = 0;

        EnemyManager.Instance?.Register(this);
    }

    protected virtual void OnDisable()
    {
        isAlive = false;
        EnemyManager.Instance?.Unregister(this);
    }

    protected override void Update()
    {
        if (!enabled) return;
        OnMove();
    }


    protected abstract void OnMove();

    protected abstract void OnDie();

    protected abstract void OnHit();
    

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        isAlive = false;

        EnemyManager.Instance.Unregister(this);
        OnDie();

        PoolManager.Instance.ReleaseToPool(enemyConfig.EnemyName, PoolableObject);
    }


    public void ReachEndPoint()
    {
        isAlive = false;
        EnemyManager.Instance.Unregister(this);

        PoolManager.Instance.ReleaseToPool(enemyConfig.EnemyName, PoolableObject);
    }
}


public enum EnemyType
{
    Bat, Bird, Cattle, Dog, Feline, Giant, Insect, Scorpion
}

