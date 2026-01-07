using System;
using UnityEngine;

public class TankDestroyControl : TurretBase
{
    [SerializeField] private Transform firePoint;

    public override void Init(TurretConfig config)
    {
        base.Init(config);
    }

    protected override void HandleFire()
    {
        if (CurrentTarget == null) return;

        HandleRotate(); 

        FireCooldown -= Time.deltaTime;
        if (FireCooldown > 0) return;

        Attack();
        FireCooldown = CurrentTurretLevel.fireRate;
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

        var level = TurretConfig.turretLevels[CurrentLevel];

        var data = new TankBulletData(
            level.bulletPoolName,  
            firePoint,                 
            CurrentTarget,             
            CurrentTurretLevel.bulletSpeed,
            CurrentTurretLevel.damage
        );

        PoolManager.Instance.Spawn(level.bulletPoolName, data);
    }

}