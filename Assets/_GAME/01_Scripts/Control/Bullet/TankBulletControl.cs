using UnityEngine;

public class TankBulletData : IPoolableData
{
    public string bulletPoolName { get; private set; } // <--- Thêm cái này
    public Transform parent { get; private set; }
    public EnemyBase target { get; private set; }
    public float speed { get; private set; }
    public int damage { get; private set; }

    public TankBulletData(string poolName, Transform parent, EnemyBase target, float speed, int damage)
    {
        this.bulletPoolName = poolName; // <--- Lưu lại
        this.parent = parent;
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }
}
public class TankBulletControl : PoolableObject
{
    private EnemyBase target;
    private float speed;
    private int damage;
    private bool isHit;
    private string myPoolName;

    public override void OnSpawn(IPoolableData ipoolData)
    {
        base.OnSpawn(ipoolData);
        var data = ipoolData as TankBulletData;
        if (data == null) { DespawnImmediate(); return; }

        myPoolName = data.bulletPoolName; 
        transform.position = data.parent.position;
        target = data.target;
        speed = data.speed;
        damage = data.damage;
        isHit = false;

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

        float step = speed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= step)
        {
            HitTarget(target);
            return;
        }

        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.position += dir * step;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
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

    private void HitTarget(EnemyBase enemy)
    {
        if (isHit) return;
        isHit = true; 
        if (enemy!=null && !enemy.IsDead && enemy.HPControl != null)
        {
            enemy.HPControl.TakeDamage(damage);
        }
        else
        {
            Debug.LogError("Enemy không có HPControl!");
        }

        SpawnExplosion();
        DespawnImmediate();
    }

    private void SpawnExplosion()
    {
        // Tạm thời comment nếu chưa setup Effect để tránh lỗi null pool
        if (PoolManager.Instance != null)
        {
             EffectData effectData = new EffectData(transform.position);
             PoolManager.Instance.Spawn(CONSTANT.EffectName.TankExplosion, effectData);
        }
    }

    private void DespawnImmediate()
    {
        // Logic thu hồi về pool
        target = null;
        if(!string.IsNullOrEmpty(myPoolName))
        {
             PoolManager.Instance.Despawn(myPoolName, this);
             PoolManager.Instance.ReleaseToPool(myPoolName, this);
        }
        else
        {
             gameObject.SetActive(false); // Fallback nếu chưa setup pool name
        }
    }
}