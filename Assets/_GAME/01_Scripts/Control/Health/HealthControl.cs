using System;
using System.Collections;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    [SerializeField] private Transform hudPosition;
    [Header("Log Info")]
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    private RectTransform hudAnchor;
    private HpHubPoolData hudData;
    public void Init(float newMaxHp)
    {
        maxHp = newMaxHp;
        currentHp = maxHp;
        hudData = new HpHubPoolData(hudPosition, UIManager.Instance.GetUI<CanvasGamePlay>().HUDAnchor);
    }
    public event Action OnDead;
    public void TakeDamage(float damage)
    {
        // Aggregate quick successive hits to avoid large, jarring HP jumps
        ScheduleDamage(damage);
    }

    // --- Damage aggregation ---
    private float pendingDamage = 0f;
    private bool damageScheduled = false;
    private const float damageAggregateDelay = 0.05f; // seconds

    private void ScheduleDamage(float damage)
    {
        pendingDamage += damage;
        if (!damageScheduled)
        {
            damageScheduled = true;
            StartCoroutine(ApplyPendingDamage());
        }
    }

    private IEnumerator ApplyPendingDamage()
    {
        yield return new WaitForSeconds(damageAggregateDelay);

        float dmg = pendingDamage;
        pendingDamage = 0f;
        damageScheduled = false;

        hudData.SetValue(currentHp, currentHp - dmg, maxHp);
        currentHp -= dmg;
        PoolManager.Instance.Spawn(nameof(HpHub), hudData);
        if (currentHp <= 0)
        {
            Dead();
        }
    }
    public bool IsFullHp => currentHp >= maxHp;
    public void Heal(float heal)
    {
        hudData.SetValue(currentHp, currentHp + heal, maxHp);
        currentHp += heal;
        PoolManager.Instance.Spawn(nameof(HpHub), hudData);
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }
    public void Dead()
    {
        currentHp = 0;
        OnDead?.Invoke();
    }
}