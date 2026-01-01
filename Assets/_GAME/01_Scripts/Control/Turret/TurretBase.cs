
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBase : MonoBehaviour
{
    protected TurretConfig TurretConfig;
    protected EnemyBase CurrentTarget;
    protected int CurrentLevel;
    protected TurretLevel CurrentTurretLevel;

    protected float FireRate;

    protected float RangeAttack;
    // [SerializeField] public int level;
    // [SerializeField] public int damage;
    // [SerializeField] public int cost;
    // [SerializeField] public float radius;
    // [SerializeField] public float fireRate;
    public virtual void Init(TurretConfig config)
    {
        this.TurretConfig = config;
        this.CurrentLevel = 0;
        this.CurrentTurretLevel = config.turretLevels[CurrentLevel];
        this.FireRate = CurrentTurretLevel.fireRate;
        this.RangeAttack = CurrentTurretLevel.range;
    }

    protected virtual void Update()
    {
        if (CurrentTarget == null)
        {
            FindTarget();
            return;
        }

        if (!IsValidEnemy(CurrentTarget))
        {
            CurrentTarget = null;
            return;
        }
        HandleFire();
    }

    protected virtual void HandleFire()
    {

    }

    protected void HandleRotate()
    {
        Vector3 direction = CurrentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle-90);
    }
    protected virtual void Attack(Action callback = null)
    {
        // follow by turret type
    }

    protected virtual void UpgradeLevel(string notice = null, Action callback = null)
    {
        if (CurrentLevel < TurretConfig.levelMax) CurrentLevel++;
        else
        {
            GameEventManager.OnLevelMaxUpdated?.Invoke(notice);
        }
    }

    protected virtual void SellTurretBase(string notice = null, Action callback = null)
    {
        var cost = TurretConfig.turretLevels[CurrentLevel].cost * 2/3;
        
    }
    private void FindTarget()
    {
        if (EnemyManager.Instance == null) return;

        var enemies = EnemyManager.Instance.activeEnemies;
        if (enemies == null || enemies.Count == 0) return;

        float minDist = float.MaxValue;
        EnemyBase nearestEnemy = null;

        foreach (var e in enemies)
        {
            if (!IsValidEnemy(e)) continue;

            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist<= RangeAttack)
            {
                minDist = dist;
                nearestEnemy = e;
                break;
            }
        }

        CurrentTarget = nearestEnemy;
    }

    private bool IsValidEnemy(EnemyBase enemy)
    {
        if (enemy == null) return false;
        if (!enemy.gameObject.activeInHierarchy) return false;
        if (enemy.IsDead) return false;

        float dist = Vector3.Distance(transform.position, enemy.transform.position);
        if (dist > RangeAttack) return false;

        return true;
    }


    protected bool IsTargetInRange()
    {
        return Vector3.Distance(transform.position, CurrentTarget.transform.position) <= CurrentTurretLevel.range;
    }


}
