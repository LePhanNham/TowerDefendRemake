using System;
using UnityEngine;

public class TankAimControl : TurretBase
{
    [SerializeField] private Transform turretHead; // Đầu xoay
    [SerializeField] private Transform firePoint;  // Đầu nòng
    [SerializeField] private float rotationSpeed = 200f;

    public override void Init(TurretConfig config)
    {
        base.Init(config);
    }

    protected override void HandleFire()
    {
        if (CurrentTarget == null) return;

        RotateTurret();

        FireCooldown -= Time.deltaTime;

        if (FireCooldown > 0) return;

        Attack();
        FireCooldown = CurrentTurretLevel.fireRate;
    }

    private void RotateTurret()
    {
        Vector3 dir = CurrentTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90); 
        
        turretHead.rotation = Quaternion.RotateTowards(
            turretHead.rotation, 
            targetRotation, 
            rotationSpeed * Time.deltaTime
        );
    }

    private bool IsAimed()
    {
        Vector3 dirToTarget = (CurrentTarget.transform.position - transform.position).normalized;
        return Vector3.Dot(firePoint.up, dirToTarget) > 0.95f; 
    }

    protected override void Attack(Action callback = null)
    {
        if (CurrentTarget == null) return;
        SpawnBullet();
        callback?.Invoke();
    }

    private void SpawnBullet()
    {
        if (CurrentTarget == null) return;

        if (firePoint == null)
        {
            Debug.LogError("TankAimControl.SpawnBullet: firePoint is not assigned.", this);
            return;
        }

        var level = TurretConfig.turretLevels[CurrentLevel];
        if (level == null)
        {
            Debug.LogError("TankAimControl.SpawnBullet: turret level data is null.", this);
            return;
        }

        string poolName = level.bulletPoolName;
        if (string.IsNullOrEmpty(poolName))
        {
            Debug.LogError("TankAimControl.SpawnBullet: bulletPoolName is empty.", this);
            return;
        }

        if (PoolManager.Instance == null)
        {
            Debug.LogError("TankAimControl.SpawnBullet: PoolManager.Instance is null.", this);
            return;
        }

        var data = new TankBulletData(
            level.bulletPoolName,
            firePoint,
            CurrentTarget,
            CurrentTurretLevel.bulletSpeed,
            CurrentTurretLevel.damage
        );

        PoolManager.Instance.Spawn(level.bulletPoolName, data);
            if (SoundManager.Instance != null) SoundManager.Instance.Play(SoundManager.SoundId.Fire);
    }
}