using UnityEngine;

using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base Enemy Stats")] 
    private EnemyType enemyType;
    protected float maxHP;
    protected float currentHP;
    protected bool isDead = false;

    public float MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    protected virtual void Awake()
    {
        currentHP = maxHP;
    }

    protected virtual void Start()
    {
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
    
    public virtual void TakeDamage(float dmg)
    {
        if (isDead) return;

        currentHP -= dmg;
        OnHit();

        if (currentHP <= 0)
        {
            Die();
        }
    }

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

