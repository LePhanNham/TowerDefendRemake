using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class LosePanelSimpleAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float zoomDuration = 0.4f;
    [SerializeField] private Ease zoomEase = Ease.OutBack;
    [SerializeField] private float fadeDuration = 0.2f;

    private CanvasGroup panelCanvasGroup;
    private RectTransform panelRect;
    private Tween currentTween;

    void Awake()
    {
        panelCanvasGroup = GetComponent<CanvasGroup>();
        panelRect = GetComponent<RectTransform>();

        panelRect.localScale = Vector3.zero;
        panelCanvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void ShowLosePanel()
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        gameObject.SetActive(true);

        panelRect.localScale = Vector3.zero;
        panelCanvasGroup.alpha = 0f;

        currentTween = DOTween.Sequence()
            .Append(panelRect.DOScale(1f, zoomDuration).SetEase(zoomEase))
            .Join(panelCanvasGroup.DOFade(1f, fadeDuration))
            .Play();
    }

    public void HideLosePanel()
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        currentTween = DOTween.Sequence()
            .Append(panelRect.DOScale(0f, zoomDuration * 0.8f).SetEase(zoomEase))
            .Join(panelCanvasGroup.DOFade(0f, fadeDuration * 0.8f))
            .OnComplete(() => {
                if (gameObject != null)
                    gameObject.SetActive(false);
            })
            .Play();
    }

    [ContextMenu("Test Show Lose Panel (Simple)")]
    private void TestShow()
    {
        ShowLosePanel();
    }

    [ContextMenu("Test Hide Lose Panel (Simple)")]
    private void TestHide()
    {
        HideLosePanel();
    }
}