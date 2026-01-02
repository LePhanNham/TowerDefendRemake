using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BallistaControl : TurretBase
{
    [FormerlySerializedAs("_ballistaPrefab")] [SerializeField] private GameObject ballistaBulletPrefab;
    [SerializeField] private Transform firePoint;
    private BallistaBinding ballistaBinding;

    private void Awake()
    {
        ballistaBinding = GetComponent<BallistaBinding>();
    }

    private void OnEnable()
    {
        ballistaBinding.SetAnimAttack();
    }

    public override void Init(TurretConfig config)
    {
        base.Init(config);
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
