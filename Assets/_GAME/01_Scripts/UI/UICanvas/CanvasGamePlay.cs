using System;
using TMPro;
// using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform hubAnchor;
    [SerializeField] private IntEventControl onWaveCompleted;
    [SerializeField] private Button waveButton;
    [SerializeField] private int currentWaveIndex;
    [SerializeField] private TextMeshProUGUI goldtText;
    // [SerializeField] private TextMeshProUGUI waveText;
    public RectTransform HUDAnchor { get => hubAnchor; set => hubAnchor = value; }

    protected override void Awake()
    {
        base.Awake();
        waveButton.GetComponent<TutorialTarget>().SetID("StartBtn");
        waveButton.onClick.AddListener(() =>
        {
            EnemySpawner.Instance.SpawnLevel();
            waveButton.gameObject.SetActive(false);
            TutorialManager.Instance.ReportAction(TutorialActionType.StartWave);
        });
        // waveButton.gameObject.SetActive(true);
    }

    public void OnEnable()
    {
        onWaveCompleted.Subscribe(SetUpBtn);
        GameEventManager.onUseMoneyUpdated+=UseGold;
        GameEventManager.onAddMoneyUpdated+=AddGold;
        
    }

    private void OnDisable()
    {
        onWaveCompleted.Unsubscribe(SetUpBtn);
        GameEventManager.onUseMoneyUpdated-=UseGold;
        GameEventManager.onAddMoneyUpdated-=AddGold;
    }

    private void UseGold(int gold)
    {
        int currentGold = EconomyManager.Instance.CurrentEconomy - gold;
        goldtText.text = currentGold.ToString();
    }
    private void AddGold(int gold)
    {
        int currentGold = EconomyManager.Instance.CurrentEconomy + gold;
        goldtText.text = currentGold.ToString();
    }
    private void SetUpBtn(int currentWave)
    {
        // waveText.text = currentWave.ToString();
        if (currentWave < EnemySpawner.Instance.MaxWaveIndex) waveButton.gameObject.SetActive(true);
        else
        {
            // Handle Win Lose
        }
    }
    
    
}