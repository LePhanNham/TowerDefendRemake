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
    
    [Header("Get Data")]
    public bool IsAlive => isAlive;
    public EnemyConfig EnemyConfig => enemyConfig;
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
        if (enemyConfig != null)
            HpControl.Init(enemyConfig.Hp);
        else
            HpControl.Init(100);
        EnemyManager.Instance?.Register(this);
        HPControl.OnDead += Die;
    }

    protected virtual void OnDisable()
    {
        isAlive = false;
        EnemyManager.Instance?.Unregister(this);
        HPControl.OnDead -= Die;
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
        GameEventManager.UpdatedEnemiesDie();
        PoolManager.Instance.Despawn(enemyConfig.EnemyName, PoolableObject);
        PoolManager.Instance.ReleaseToPool(enemyConfig.EnemyName, PoolableObject);
    }


    public void ReachEndPoint()
    {
        isAlive = false;
        EnemyManager.Instance.Unregister(this);
        PoolManager.Instance.Despawn(enemyConfig.EnemyName, PoolableObject);
        PoolManager.Instance.ReleaseToPool(enemyConfig.EnemyName, PoolableObject);
    }
}


public enum EnemyType
{
    Bat, Bird, Cattle, Dog, Feline, Giant, Insect, Scorpion
}

