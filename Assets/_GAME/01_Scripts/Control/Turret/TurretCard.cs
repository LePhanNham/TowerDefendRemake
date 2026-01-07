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
        if (EconomyManager.Instance.CurrentEconomy >= int.Parse(cost.text))
        {
            var turretGo = Instantiate(turretConfig.turretPrefab, TurretCardPanel.Instance.StartPos.BuildPosition, Quaternion.identity);
            var turret = turretGo.GetComponent<TurretBase>();
            turret.Init(turretConfig);
            TurretCardPanel.Instance.Hide();
            GameEventManager.BuildTurretCompleted(TurretCardPanel.Instance.StartPos);
            GameEventManager.UseMoneyUpdated(int.Parse(cost.text));
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
