using UnityEngine;

using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Enemy Stats")] 
    private EnemyType enemyType;

    protected HealthControl hpControl;
    protected bool isDead = false;

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

    protected virtual void Awake()
    {
        hpControl = GetComponent<HealthControl>();
    }
    protected virtual void Update()
    {
        if (!isDead)
        {
            OnMove();
        }
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
}


public enum EnemyType
{
    Bat, Bird, Cattle, Dog, Feline, Giant, Insect, Scorpion
}

