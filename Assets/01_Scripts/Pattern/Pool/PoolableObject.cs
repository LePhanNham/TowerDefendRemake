using UnityEngine;

public interface IPoolableData
{
    
}
public class PoolableObject : MonoBehaviour
{
    public virtual void OnSpawn(IPoolableData ipoolData)
    {
        
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}