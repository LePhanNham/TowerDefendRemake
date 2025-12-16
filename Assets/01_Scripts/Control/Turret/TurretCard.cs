using System;
using UnityEngine;
using UnityEngine.UI;

public class TurretCard : MonoBehaviour
{
    [SerializeField] public string nameTurret;
    [SerializeField] public Image spriteRenderer;
    [SerializeField] public TurretBase turretPrefab;
    [SerializeField] public int cost;

    public void SetUp(TurretConfig config)
    {
        nameTurret = config.nameTurret;
        spriteRenderer.sprite = config.spriteRenderer;
        cost = config.costBaseTurret;
    }

    public void OnClick()
    {
        Instantiate(turretPrefab, TurretCardPanel.Instance.StartPos, Quaternion.identity);
    }
}
