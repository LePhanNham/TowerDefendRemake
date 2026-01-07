using UnityEngine;

public class MissileSkill : SkillBaseControl
{
    private Vector3 startPos;

    protected override void OnSkillStart()
    {
        startPos = targetPos + new Vector3(0, 15f, 0); 
        transform.position = startPos;
        
        float angle = Mathf.Atan2(targetPos.y - startPos.y, targetPos.x - startPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isFinished) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, config.speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        isFinished = true;
        
        ApplyAreaEffect(transform.position, config.radius, config.powerValue);
        PoolManager.Instance.Spawn(CONSTANT.EffectName.TankExplosion, new EffectData(transform.position));
        DespawnSkill();
    }
    private void OnDrawGizmosSelected()
    {
        if(config != null) Gizmos.DrawWireSphere(transform.position, config.radius);
    }
}