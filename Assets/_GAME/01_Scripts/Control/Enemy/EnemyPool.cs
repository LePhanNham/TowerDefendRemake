
using UnityEngine;

public class EnemyPool : PoolableObject
{
    private EnemyPoolData data;
    
}

public class EnemyPoolData : IPoolableData
{
    private Vector3 position;
    private string poolName;
    public EnemyPoolData(string poolName, Vector3 position)
    {
        this.poolName = poolName;
        this.position = position;
    }

    public void SetValue(string poolName, Vector3 position)
    {
        this.poolName = poolName;
        this.position = position;
    }
    public Vector3 Position => position;
    public string PoolName => poolName;
}
