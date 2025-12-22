using System;
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
        hudData.SetValue(currentHp, currentHp - damage, maxHp);
        currentHp -= damage;
        PoolManager.Instance.Spawn(nameof(HpHub), hudData);
        if (currentHp <= 0)
        {
            OnDead?.Invoke();
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