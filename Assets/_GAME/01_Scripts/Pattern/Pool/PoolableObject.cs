using UnityEngine;

public interface IPoolableData
{
    
}
public class PoolableObject : MonoBehaviour
{
    public virtual void OnSpawn(IPoolableData ipoolData)
    {
        gameObject.SetActive(true);
    }

    public virtual void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}