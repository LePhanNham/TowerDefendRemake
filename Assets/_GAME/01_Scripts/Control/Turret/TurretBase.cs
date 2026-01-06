
using System;
using UnityEngine;


public class TurretBase : MonoBehaviour
{
    // panelInformation removed: use shared TurretInformation.Instance instead
    protected TurretConfig TurretConfig;
    protected EnemyBase CurrentTarget;
    protected int CurrentLevel;
    protected TurretLevel CurrentTurretLevel;
    protected float RangeAttack;
    protected float FireCooldown;
    public virtual void Init(TurretConfig config)
    {
        this.TurretConfig = config;
        this.CurrentLevel = 0;
        this.CurrentTurretLevel = config.turretLevels[CurrentLevel];
        this.RangeAttack = CurrentTurretLevel.range;
        FireCooldown = 0;
        GetComponent<TutorialTarget>().SetID(CONSTANT.TutorialMessage.step_5);
        if (TurretInformation.Instance != null)
            TurretInformation.Instance.UpdateUI(CurrentTurretLevel);
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
        
    }

    public void UpgradeLevel(string notice = null, Action callback = null)
    {
        if (CurrentLevel < TurretConfig.levelMax)
        {
            if (EconomyManager.Instance.CurrentEconomy >= TurretConfig.turretLevels[CurrentLevel + 1].cost)
            {
                GameEventManager.UseMoneyUpdated(TurretConfig.turretLevels[CurrentLevel + 1].cost);
                CurrentLevel++;
                SetCurrentLevel(CurrentLevel);
                callback?.Invoke();
                TutorialManager.Instance.ReportAction(TutorialActionType.UpgradeTurret);
                if (TurretInformation.Instance != null)
                    TurretInformation.Instance.UpdateUI(CurrentTurretLevel);
                
            }
            else
            {
                GameEventManager.ShowUnableToUpgrade("Not enough Money to Upgrade Turret");
            }
        }
        else
        {
            GameEventManager.LevelMaxUpdated(notice);
        }
        
    }

    public void SetCurrentLevel(int level)
    {
        CurrentTurretLevel = TurretConfig.turretLevels[level];
        FireCooldown = CurrentTurretLevel.fireRate;
        RangeAttack = CurrentTurretLevel.range;
    }
    public TurretLevel GetCurrentTurretLevel() {
        return CurrentTurretLevel;
    }
    public void SellTurretBase(string notice = null, Action callback = null)
    {
        var cost = CurrentTurretLevel.cost * 2/3;
        EconomyManager.Instance.AddMoney(cost);
        TutorialManager.Instance.ReportAction(TutorialActionType.SellTurret);
        callback?.Invoke();
        Destroy(gameObject);
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
            if (dist < minDist && dist <= RangeAttack)
            {
                minDist = dist;
                nearestEnemy = e;
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
