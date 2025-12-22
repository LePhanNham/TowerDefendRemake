using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : SingletonMono<PoolManager>
{
    [SerializeField] private List<PoolControl> poolInfors = new List<PoolControl>();
    // [SerializeField] private List<PoolableObject> poolList = new List<PoolableObject>();
    private Dictionary<string, PoolControl> poolMapping = new Dictionary<string, PoolControl>();

    protected override void Awake()
    {
        base.Awake();
        foreach (var pool in poolInfors)
        {
            pool.Initialize(transform);
            poolMapping[pool.PoolName] = pool;
        }
    }

    public PoolableObject Spawn(string poolName, IPoolableData data)
    {
        if (poolMapping.ContainsKey(poolName))
        {
            return poolMapping[poolName].Spawn(data);
        }
        return null;
    }

    public void Despawn(string poolName, PoolableObject obj)
    {
        if (poolMapping.TryGetValue(poolName, out var pool))
        {
            pool.Despawn(obj);
        }
    }

    public void ReleaseToPool(string poolName, PoolableObject obj)
    {
        if (poolMapping.TryGetValue(poolName, out var pool))
        {
            pool.ReleaseToPool(obj);
        }
    }
    
}