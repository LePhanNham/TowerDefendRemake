using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class HomePanelAnimator : MonoBehaviour
{
    [Header("Elements (Kéo từ Hierarchy)")]
    [SerializeField] private RectTransform startButton;
    [SerializeField] private RectTransform exitButton;

    [Header("Animation Settings")]
    [SerializeField] private float panelInDuration = 0.6f;
    [SerializeField] private Ease panelEase = Ease.OutBack;
    
    [SerializeField] private float buttonInDuration = 0.4f;
    [SerializeField] private float buttonDelay = 0.15f;
    [SerializeField] private Ease buttonEase = Ease.OutBack;

    private CanvasGroup homePanelCanvasGroup;
    private RectTransform homePanelRect;
    private Sequence homeSequence;

    void Awake()
    {
        homePanelCanvasGroup = GetComponent<CanvasGroup>();
        homePanelRect = GetComponent<RectTransform>();

        SetInitialStates();
    }

    private void SetInitialStates()
    {
        homePanelRect.localScale = Vector3.zero;
        homePanelCanvasGroup.alpha = 0f;

        startButton.localScale = Vector3.zero;
        exitButton.localScale = Vector3.zero;
    }

    public void ShowHomePanel()
    {
        gameObject.SetActive(true);
        SetInitialStates();

        homeSequence = DOTween.Sequence();

        homeSequence.Append(homePanelRect.DOScale(1f, panelInDuration).SetEase(panelEase));
        homeSequence.Join(homePanelCanvasGroup.DOFade(1f, panelInDuration * 0.8f));

        homeSequence.Append(startButton.DOScale(1f, buttonInDuration).SetEase(buttonEase));
        homeSequence.AppendInterval(buttonDelay);
        homeSequence.Append(exitButton.DOScale(1f, buttonInDuration).SetEase(buttonEase));
        
        homeSequence.Play();
    }

    public void HideHomePanel()
    {
        if (homeSequence != null && homeSequence.IsActive())
        {
            homeSequence.Kill();
        }

        homeSequence = DOTween.Sequence();

        homeSequence.Append(exitButton.DOScale(0f, buttonInDuration).SetEase(buttonEase));
        homeSequence.AppendInterval(buttonDelay / 2);

        homeSequence.Append(startButton.DOScale(0f, buttonInDuration).SetEase(buttonEase));

        homeSequence.Append(homePanelRect.DOScale(0f, panelInDuration).SetEase(panelEase));
        homeSequence.Join(homePanelCanvasGroup.DOFade(0f, panelInDuration * 0.8f));

        homeSequence.OnComplete(() => gameObject.SetActive(false));

        homeSequence.Play();
    }

    [ContextMenu("Test Show Home Panel")]
    private void TestShow()
    {
        ShowHomePanel();
    }
    
    [ContextMenu("Test Hide Home Panel")]
    private void TestHide()
    {
        HideHomePanel();
    }
}