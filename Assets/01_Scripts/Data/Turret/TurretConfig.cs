
using System;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] public float radius;
    [SerializeField] public float fireRate;

    public TurretLevel(int level, int damage, int cost, float radius, float fireRate)
    {
        this.level = level;
        this.damage = damage;
        this.cost = cost;
        this.radius = radius;
        this.fireRate = fireRate;
    }
}