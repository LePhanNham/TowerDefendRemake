using UnityEngine;

public abstract class SkillBaseControl : PoolableObject
{
    protected Vector3 targetPos;
    protected SkillConfig config;
    protected bool isFinished;

    public override void OnSpawn(IPoolableData ipoolData)
    {
        base.OnSpawn(ipoolData);
        var data = ipoolData as SkillSpawnData;
        if (data == null) { DespawnSkill(); return; }

        this.targetPos = data.targetPosition;
        this.config = data.config;
        this.isFinished = false;

        OnSkillStart(); 
    }

    protected abstract void OnSkillStart();

    protected void ApplyAreaEffect(Vector3 center, float radius, int value)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, radius);
        foreach (var hit in hits)
        {
            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null && !enemy.IsDead)
            {
                enemy.HPControl.TakeDamage(value);
            }
        }
    }

    protected void DespawnSkill()
    {
        if (config != null)
        {
            PoolManager.Instance.Despawn(config.poolName, this);
            PoolManager.Instance.ReleaseToPool(config.poolName, this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}