
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TurretConfig", menuName = "TurretConfig")]
public class TurretConfig : ScriptableObject
{
    [SerializeField] public int id;
    [SerializeField] public int costBaseTurret;
    [SerializeField] public int levelMax;
    [SerializeField] public List<TurretLevel> turretLevels;
    
    [Header("Information")]
    [SerializeField] public string nameTurret;
    [SerializeField] public Sprite spriteRenderer;
    [SerializeField] public TurretBase turretPrefab;
}
[Serializable]
public class TurretLevel
{
    [SerializeField] public int level;
    [SerializeField] public int damage;
    [SerializeField] public int cost;
    [SerializeField] public float range;
    [SerializeField] public float fireRate;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public string bulletPoolName;
    [FormerlySerializedAs("BulletSpeed")] [SerializeField] public float bulletSpeed;

    public TurretLevel(int level, int damage, int cost, float range, float fireRate)
    {
        this.level = level;
        this.damage = damage;
        this.cost = cost;
        this.range = range;
        this.fireRate = fireRate;
    }
}

[System.Serializable]
public class TowerStatsRuntime
{
    public int level;
    public float damage;
    public float fireRate;
    public float range;
    public string bulletPoolName;

    public void Apply(TurretLevel upgrade)
    {
        level = upgrade.level;
        damage = upgrade.damage;
        fireRate = upgrade.fireRate;
        range = upgrade.range;
        bulletPoolName = upgrade.bulletPoolName;
    }
}
