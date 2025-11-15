using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolControl
{
    [SerializeField] private string poolName;
    [SerializeField] private int poolSize;
    [SerializeField] private PoolableObject prefab;
    
    private Queue<PoolableObject> poolCollection = new Queue<PoolableObject>();
    private Transform poolParent;
    public string PoolName { get => poolName; set => poolName = value; }
    public int PoolSize { get => poolSize; set => poolSize = value; }
    public PoolableObject Prefab { get => prefab; set => prefab = value; }
    public void Initialize(Transform poolParent)
    {
        this.poolParent = poolParent;
        poolCollection.Clear();
        for (int i = 0; i < poolSize; i++)
        {
            PoolableObject obj = UnityEngine.Object.Instantiate(prefab, poolParent);
            obj.gameObject.SetActive(false);
            poolCollection.Enqueue(obj);
        }
    }

    public PoolableObject Spawn(IPoolableData ipoolData)
    {
        PoolableObject obj = poolCollection.Count > 0
            ? poolCollection.Dequeue()
            : UnityEngine.Object.Instantiate(prefab, poolParent);
        obj.OnSpawn(ipoolData);
        return obj;
    }

    public void Despawn(PoolableObject obj)
    {
        if (obj == null)
        {
            return;
        }
        obj.OnDespawn();
    }
    
    public void ReleaseToPool(PoolableObject obj)
    {
        if (obj == null)
        {
            return;
        }
        poolCollection.Enqueue(obj);
    }
}