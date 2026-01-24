using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretCard : MonoBehaviour
{
    [SerializeField] public Image spriteRenderer;
    [SerializeField] public TurretConfig turretConfig;
    [SerializeField] public TextMeshProUGUI cost;
    [SerializeField] public Button btnListener;
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        btnListener.onClick.AddListener(OnClick);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;  
        btnListener.interactable = true;
    }


    public void SetUp(TurretConfig config)
    {
        spriteRenderer.sprite = config.spriteRenderer;
        cost.text = config.costBaseTurret.ToString();
        turretConfig = config;
    }

    public void OnClick()
    {
        if (EconomyManager.Instance.CurrentEconomy >= turretConfig.costBaseTurret)
        {
            // ensure spawn Z is 0 (some prefabs or nodes may carry non-zero Z)
            var spawnPos = TurretCardPanel.Instance.StartPos.BuildPosition;
            spawnPos.z = 0f;
            var turretGo = Instantiate(turretConfig.turretPrefab, spawnPos, Quaternion.identity);
            turretGo.transform.position = spawnPos;
            var turret = turretGo.GetComponent<TurretBase>();
            if (turret != null)
            {
                turret.HostNode = TurretCardPanel.Instance.StartPos;
                TurretCardPanel.Instance.StartPos.Occupy(turret);
            }
            turret.Init(turretConfig);
            TurretCardPanel.Instance.Hide();
            GameEventManager.BuildTurretCompleted(TurretCardPanel.Instance.StartPos);
            GameEventManager.UseMoneyUpdated(turretConfig.costBaseTurret);
            if (SoundManager.Instance != null) SoundManager.Instance.Play(SoundManager.SoundId.Build);
            TutorialManager.Instance.ReportAction(TutorialActionType.BuildTower);
        }
        else
        {
            ShowMessage(CONSTANT.Message.UnableToBuy);
        }
    }

    public void ShowMessage(string message)
    {
        GameEventManager.ShowUnableToBuy(message);
    }
}
