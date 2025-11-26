using System;
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
    // [SerializeField] private TextMeshProUGUI waveText;
    public RectTransform HUDAnchor { get => hubAnchor; set => hubAnchor = value; }

    protected override void Awake()
    {
        base.Awake();
        waveButton.onClick.AddListener(() =>
        {
            EnemySpawner.Instance.SpawnLevel();
            waveButton.gameObject.SetActive(false);
        });
    }

    public void OnEnable()
    {
        onWaveCompleted.Subscribe(SetUpBtn);
    }

    private void OnDisable()
    {
        onWaveCompleted.Unsubscribe(SetUpBtn);
    }

    private void SetUpBtn(int currentWave)
    {
        // waveText.text = currentWave.ToString();
        waveButton.gameObject.SetActive(true);
    }
}