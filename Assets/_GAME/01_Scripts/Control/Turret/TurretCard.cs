using System;
using UnityEngine;
using UnityEngine.UI;

public class TurretCard : MonoBehaviour
{
    [SerializeField] public string nameTurret;
    [SerializeField] public Image spriteRenderer;
    [SerializeField] public TurretConfig turretConfig;
    [SerializeField] public int cost;
    [SerializeField] public Button btnListener;
    
    private void Awake()
    {
        btnListener.onClick.AddListener(OnClick);
}

    public void SetUp(TurretConfig config)
    {
        nameTurret = config.nameTurret;
        spriteRenderer.sprite = config.spriteRenderer;
        cost = config.costBaseTurret;
        turretConfig = config;
    }

    public void OnClick()
    {
        var turretGo = Instantiate(turretConfig.turretPrefab, TurretCardPanel.Instance.StartPos.BuildPosition, Quaternion.identity);
        var turret = turretGo.GetComponent<TurretBase>();
        turret.Init(turretConfig);
        TurretCardPanel.Instance.Hide();
        GameEventManager.OnOnBuildTurretCompleted(TurretCardPanel.Instance.StartPos);
    }
    
}
