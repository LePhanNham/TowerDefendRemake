using UnityEngine;

public class ExplosionEffectControl : PoolableObject
{
    [SerializeField] private float duration = 1.0f; 

    public override void OnSpawn(IPoolableData ipoolData)
    {
        base.OnSpawn(ipoolData);
        var data = ipoolData as EffectData;
        if (data == null)
        {
            DespawnImmediate();
            return;
        }

        transform.position = data.position;
        transform.rotation = data.rotation;
        
        gameObject.SetActive(true);

        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null) ps.Play();

        CancelInvoke(nameof(DespawnImmediate)); 
        Invoke(nameof(DespawnImmediate), duration);
    }

    private void DespawnImmediate()
    {
        PoolManager.Instance.Despawn(CONSTANT.EffectName.TankExplosion, this);
        PoolManager.Instance.ReleaseToPool(CONSTANT.EffectName.TankExplosion, this);
    }
}