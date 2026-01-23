using UnityEngine;

public class MissileSkill : SkillBaseControl
{
    private Vector3 startPos;
    public float spawnOffsetY = 0f; 
    public float maxLifetime = 5f; 
    public float despawnDelay = 2f; // wait a bit so explosion effect is visible

    protected override void OnSkillStart()
    {
        startPos = targetPos + new Vector3(0, spawnOffsetY, 0);
        transform.position = startPos;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isFinished) return;

        if (config == null)
        {
            DespawnSkill();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, config.speed * Time.deltaTime);

        float sqrDist = (transform.position - targetPos).sqrMagnitude;
        float explodeRadius = Mathf.Max(0.01f, config.radius);
        if (sqrDist <= explodeRadius * explodeRadius)
        {
            Explode();
        }
    }

    private void Explode()
    {
        isFinished = true;
        float radius = (config != null) ? config.radius : 0.1f;
        int power = (config != null) ? config.powerValue : 0;
        ApplyAreaEffect(transform.position, radius, power);
        PoolManager.Instance.Spawn(CONSTANT.EffectName.FireEffect, new EffectData(transform.position));
        StartCoroutine(DelayedDespawn());
    }

    private System.Collections.IEnumerator DelayedDespawn()
    {
        yield return new WaitForSeconds(despawnDelay);
        DespawnSkill();
    }
    private void OnDrawGizmosSelected()
    {
        if(config != null) Gizmos.DrawWireSphere(transform.position, config.radius);
        else Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}