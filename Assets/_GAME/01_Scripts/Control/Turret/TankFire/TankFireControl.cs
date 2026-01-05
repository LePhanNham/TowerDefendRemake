
using System;
using UnityEngine;
using UnityEngine.Serialization;


public class TankFireControl : TurretBase
{
    [FormerlySerializedAs("_ballistaPrefab")] [SerializeField] private GameObject ballistaBulletPrefab;
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
        SpawnArrow();           
        callback?.Invoke();
    }

    private void SpawnArrow()
    {
        if (CurrentTarget == null) return;

        var data = new BallistaBulletData(
            firePoint,
            (Vector2)(CurrentTarget.transform.position - firePoint.position),
            CurrentTarget,
            TurretConfig.turretLevels[CurrentLevel].bulletSpeed,
            TurretConfig.turretLevels[CurrentLevel].damage
        );

        PoolManager.Instance.Spawn(CONSTANT.BulletName.BallistaBullet, data);
    }

}
