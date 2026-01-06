using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TurretCardPanel : SingletonMono<TurretCardPanel>
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TurretCard cardPrefab;
    [SerializeField] private GameObject overlayBlocker;

    [SerializeField] private float panelFadeTime;
    [SerializeField] private float panelScaleTime;
    [SerializeField] private float cardFadeTime;
    [SerializeField] private float cardDelay;


    private List<TurretConfig> turretConfigs;
    private NodeBase startPos;
    public NodeBase StartPos => startPos;
    protected override void Awake()
    {
        base.Awake();
        canvasGroup.alpha = 0;
        panel.localScale = Vector3.zero;
    }

    private void Start()
    {
        LoadCards();
    }

    private void LoadCards()
    {
        turretConfigs = ConfigManager.Instance.GetTurretConfigs();

        foreach (var cfg in turretConfigs)
        {
            var card = Instantiate(cardPrefab, panel);
            card.SetUp(cfg);
        }
    }

    public void Show(NodeBase node)
    {
        startPos = node;
        panel.gameObject.SetActive(true);
        if (overlayBlocker != null) overlayBlocker.SetActive(true);
        // ensure overlay blocker is behind the panel so panel buttons receive clicks
        if (overlayBlocker != null)
            overlayBlocker.transform.SetAsLastSibling();
        panel.SetAsLastSibling();
        DOTween.Kill(panel);
        DOTween.Kill(canvasGroup);
        // reset trang thai
        canvasGroup.alpha = 0;
        panel.localScale = Vector3.zero;
        
        // anim panel
        canvasGroup.DOFade(1f, panelFadeTime);
        panel.DOScale(1, panelScaleTime).SetEase(Ease.OutBack);
        
        for (int i = 0; i < panel.childCount; i++)
        {
            var child = panel.GetChild(i).GetComponent<TurretCard>();
            var tutor = child.GetComponent<TutorialTarget>();
            if (i==0) tutor.SetID(CONSTANT.TutorialMessage.step_2);
            FadeCardIn(child, i* cardDelay);
        }
    }

    private void FadeCardIn(TurretCard card, float delay)
    {
        card.gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        var canvasG = card.GetComponent<CanvasGroup>();
        canvasG.DOKill();
        canvasG.alpha = 0;
        canvasG.DOFade(1, cardFadeTime).SetDelay(delay);
    }

    public void Hide()
    {
        overlayBlocker.SetActive(false);
        for (int i = 0; i < panel.childCount; i++)
        {
            var child = panel.GetChild(i).GetComponent<TurretCard>();
            FadeCardOut(child, cardDelay);
        }
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, panelFadeTime)
            .OnComplete(() =>
            {
                panel.gameObject.SetActive(false);
            });
    }
    private void FadeCardOut(TurretCard card, float delay)
    {
        var canvasG = card.GetComponent<CanvasGroup>();
        canvasG.DOKill();
        canvasG.interactable = false;
        canvasG.blocksRaycasts = false;
        canvasG.DOFade(0, 0.1f);
    }
    public void BuildCompleted()
    {
        
    }
}
