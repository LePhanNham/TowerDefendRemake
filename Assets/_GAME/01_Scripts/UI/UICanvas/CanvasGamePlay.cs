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
    [SerializeField] private UnityEngine.UI.Slider hpSlider;
    public RectTransform HUDAnchor { get => hubAnchor; set => hubAnchor = value; }

    protected override void Awake()
    {
        base.Awake();
        waveButton.GetComponent<TutorialTarget>().SetID(CONSTANT.TutorialMessage.step_3);
        waveButton.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.Play(SoundManager.SoundId.Click);
                SoundManager.Instance.Play(SoundManager.SoundId.StartWave);
            }
            EnemySpawner.Instance.SpawnLevel();
            waveButton.gameObject.SetActive(false);
            TutorialManager.Instance.ReportAction(TutorialActionType.StartWave);
        });
        // waveButton.gameObject.SetActive(true);
    }
    private void Start()
    {
        goldtText.text = EconomyManager.Instance.CurrentEconomy.ToString();
    }
    public void OnEnable()
    {
        onWaveCompleted.Subscribe(SetUpBtn);
        GameEventManager.onUseMoneyUpdated+=UseGold;
        GameEventManager.onAddMoneyUpdated+=AddGold;
        GameEventManager.onBaseHpUpdated += UpdateHp;
        GameEventManager.onBaseHpZero += OnBaseHpZero;
        if (hpSlider != null && LevelManager.Instance != null)
        {
            hpSlider.maxValue = LevelManager.Instance.BaseMaxHp;
            hpSlider.value = LevelManager.Instance.CurrentBaseHp;
        }
        if (goldtText != null && EconomyManager.Instance != null)
        {
            goldtText.text = EconomyManager.Instance.CurrentEconomy.ToString();
        }

        if (waveButton != null)
        {
            bool hasWaves = (EnemySpawner.Instance != null && EnemySpawner.Instance.MaxWaveIndex > 0);
            waveButton.gameObject.SetActive(hasWaves);
            waveButton.interactable = hasWaves;
        }
    }

    private void OnDisable()
    {
        onWaveCompleted.Unsubscribe(SetUpBtn);
        GameEventManager.onUseMoneyUpdated-=UseGold;
        GameEventManager.onAddMoneyUpdated-=AddGold;
        GameEventManager.onBaseHpUpdated -= UpdateHp;
        GameEventManager.onBaseHpZero -= OnBaseHpZero;
    }

    private void UseGold(int gold)
    {
        goldtText.text = EconomyManager.Instance.CurrentEconomy.ToString();
    }
    private void AddGold(int gold)
    {
        goldtText.text = EconomyManager.Instance.CurrentEconomy.ToString();
    }
    private void SetUpBtn(int currentWave)
    {
        if (currentWave < EnemySpawner.Instance.MaxWaveIndex) waveButton.gameObject.SetActive(true);
        else
        {
            waveButton.gameObject.SetActive(false);
            GameManager.ChangeState(GameState.Finish);
            UIManager.Instance.CloseAll();
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.StopMusic();
                SoundManager.Instance.Play(SoundManager.SoundId.Win);
            }
            UIManager.Instance.OpenUI<CanvasWin>();
        }
    }

    private void UpdateHp(int hp)
    {
        Debug.Log($"CanvasGamePlay: UpdateHp({hp})");
        if (hpSlider != null)
        {
            if (hpSlider.maxValue <= 0 && LevelManager.Instance != null)
            {
                hpSlider.maxValue = LevelManager.Instance.BaseMaxHp;
            }
            hpSlider.value = hp;
        }
    }

    private void OnBaseHpZero()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.Play(SoundManager.SoundId.Lose);
        }
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasLose>();
    }
    
    
}