using UnityEngine;

public class BallistaBulletData : IPoolableData
{
    public Transform parent { get; private set; }
    public Vector2 direction { get; private set; }
    public EnemyBase target { get; private set; }
    public float speed { get; private set; }
    public int damage { get; private set; }

    public BallistaBulletData(
        Transform parent,
        Vector2 direction,
        EnemyBase target,
        float speed,
        int damage)
    {
        this.parent = parent;
        this.direction = direction.normalized;
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }
}

[RequireComponent(typeof(Collider2D))]
public class BallistaBulletControl : PoolableObject
{
    private EnemyBase target;
    private float speed;
    private int damage;
    private bool isHit;
    private Collider2D col;

    public override void OnSpawn(IPoolableData ipoolData)
    {
        base.OnSpawn(ipoolData);

        var data = ipoolData as BallistaBulletData;
        if (data == null)
        {
            DespawnImmediate();
            return;
        }

        transform.position = data.parent.position;
        transform.rotation = Quaternion.identity;

        target = data.target;
        speed = data.speed;
        damage = data.damage;
        isHit = false;

        col = GetComponent<Collider2D>();
        col.enabled = true;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isHit) return;

        if (target == null || target.IsDead || !target.gameObject.activeInHierarchy)
        {
            DespawnImmediate();
            return;
        }

        Vector3 dir = (target.transform.position - transform.position).normalized;
        float distanceThisFrame = speed * Time.deltaTime;
        
        float distToTarget = Vector3.Distance(transform.position, target.transform.position);
        
        if (distToTarget <= distanceThisFrame)
        {
            HitTarget(target);
            return;
        }

        transform.position += dir * distanceThisFrame;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isHit) return;
        
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy == null)
        {
            enemy = other.GetComponentInParent<EnemyBase>();
        }

        if (enemy == null || enemy.IsDead) return;
        HitTarget(enemy);
    }

    private void HitTarget(EnemyBase enemy)
    {
        enemy.HPControl.TakeDamage(damage);
        if (isHit) return; // Double check
        isHit = true;
        col.enabled = false;

        DespawnImmediate();
    }

    private void DespawnImmediate()
    {
        isHit = true;
        target = null;

        gameObject.SetActive(false);
        PoolManager.Instance.ReleaseToPool(
            CONSTANT.BulletName.BallistaBullet, this);
    }
}

