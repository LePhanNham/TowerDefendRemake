using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : SingletonMono<EnemyManager>
{
    [FormerlySerializedAs("ActiveEnemies")] public List<EnemyBase> activeEnemies;

    protected override void Awake()
    {
        base.Awake();
        activeEnemies = new List<EnemyBase>();
    }

    public void Register(EnemyBase enemy)
    {
        if (!activeEnemies.Contains(enemy))
            activeEnemies.Add(enemy);
    }

    public void Unregister(EnemyBase enemy)
    {
        activeEnemies.Remove(enemy);
    }
}