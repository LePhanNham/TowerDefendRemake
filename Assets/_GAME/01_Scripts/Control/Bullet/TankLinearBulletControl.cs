using UnityEngine;

public class TankLinearBulletData : IPoolableData
{
    public string bulletPoolName { get; private set; }
    public Vector3 startPosition { get; private set; }
    public Vector3 direction { get; private set; } // Hướng bay cố định
    public float speed { get; private set; }
    public int damage { get; private set; }

    public TankLinearBulletData(string poolName, Vector3 startPos, Vector3 dir, float speed, int damage)
    {
        this.bulletPoolName = poolName;
        this.startPosition = startPos;
        this.direction = dir.normalized; // Chuẩn hóa vector để bay đều
        this.speed = speed;
        this.damage = damage;
    }
}

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TankLinearBulletControl : PoolableObject
{
    private Vector3 moveDir;
    private float speed;
    private int damage;
    private string myPoolName;
    private bool isHit;
    private Rigidbody2D rb;
    private float lifeTime = 3f; 
    public float despawnDelay = 0.25f;

    public override void OnSpawn(IPoolableData ipoolData)
    {
        base.OnSpawn(ipoolData);
        var data = ipoolData as TankLinearBulletData;
        if (data == null) { DespawnImmediate(); return; }

        myPoolName = data.bulletPoolName;
        transform.position = data.startPosition;
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.position = transform.position;
        moveDir = data.direction;
        speed = data.speed;
        damage = data.damage;
        isHit = false;
        lifeTime = 3f;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isHit) return;

        // lifetime countdown handled in Update; movement in FixedUpdate
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DespawnImmediate();
        }
    }

    private void FixedUpdate()
    {
        if (isHit) return;

        if (rb != null)
        {
            Vector2 next = rb.position + (Vector2)moveDir * speed * Time.fixedDeltaTime;
            rb.MovePosition(next);
            transform.position = rb.position;
        }
        else
        {
            transform.position += moveDir * speed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isHit) return;

        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy == null) enemy = other.GetComponentInParent<EnemyBase>();

        if (enemy != null && !enemy.IsDead)
        {
            HitTarget(enemy);
        }
    }

    private void HitTarget(EnemyBase directHitEnemy)
    {
        if (isHit) return;
        isHit = true;

        if (directHitEnemy != null && directHitEnemy.HPControl != null)
        {
            directHitEnemy.HPControl.TakeDamage(damage);
        }

        SpawnExplosion();
        StartCoroutine(DelayedDespawn());
    }

    private void SpawnExplosion()
    {
        if (PoolManager.Instance != null)
        {
            EffectData effectData = new EffectData(transform.position);
            PoolManager.Instance.Spawn(CONSTANT.EffectName.TankExplosion, effectData);
        }
    }

    private void DespawnImmediate()
    {
        if(!string.IsNullOrEmpty(myPoolName))
        {
             if (PoolManager.Instance != null)
             {
                 PoolManager.Instance.Despawn(myPoolName, this);
                 PoolManager.Instance.ReleaseToPool(myPoolName, this);
             }
        }
        else
        {
             gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator DelayedDespawn()
    {
        yield return new WaitForSeconds(despawnDelay);
        DespawnImmediate();
    }
}