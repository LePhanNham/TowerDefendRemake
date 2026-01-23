using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class WinPanelAnimator : MonoBehaviour
{
    [Header("Elements (Kéo từ Hierarchy)")]
    [SerializeField] private RectTransform winText; 
    [SerializeField] private RectTransform[] stars; 
    [SerializeField] private RectTransform restartButton; 
    [SerializeField] private RectTransform exitButton; 

    [Header("Animation Settings")]
    [SerializeField] private float panelInDuration = 0.5f;
    [SerializeField] private Ease panelEase = Ease.OutBack;
    
    [SerializeField] private float textInDuration = 0.4f;
    [SerializeField] private Ease textEase = Ease.OutBack;

    [SerializeField] private float starInDuration = 0.3f;
    [SerializeField] private float starDelay = 0.15f; 
    [SerializeField] private Ease starEase = Ease.OutBack;

    [SerializeField] private float buttonInDuration = 0.3f;
    [SerializeField] private Ease buttonEase = Ease.OutBack;

    private CanvasGroup panelCanvasGroup;
    private RectTransform panelRect;
    private Sequence winSequence; 

    void Awake()
    {
        panelCanvasGroup = GetComponent<CanvasGroup>();
        panelRect = GetComponent<RectTransform>();
        SetInitialStates();
    }
    
    private void SetInitialStates()
    {
        panelRect.localScale = Vector3.zero;
        panelCanvasGroup.alpha = 0f;
        
        winText.localScale = Vector3.zero;
        restartButton.localScale = Vector3.zero;
        exitButton.localScale = Vector3.zero;
        
        foreach (RectTransform star in stars)
        {
            star.localScale = Vector3.zero;
        }
    }
    
    public void ShowWinPanel()
    {
        gameObject.SetActive(true);
        SetInitialStates();
        winSequence = DOTween.Sequence();
        winSequence.Append(panelRect.DOScale(1f, panelInDuration).SetEase(panelEase));
        winSequence.Join(panelCanvasGroup.DOFade(1f, panelInDuration * 0.8f)); // Fade nhanh hơn 1 chút
        
        winSequence.Append(winText.DOScale(1f, textInDuration).SetEase(textEase));
        foreach (RectTransform star in stars)
        {
            winSequence.Append(star.DOScale(1f, starInDuration).SetEase(starEase));
            winSequence.AppendInterval(starDelay); // Thêm một khoảng nghỉ ngắn
        }
        winSequence.Append(restartButton.DOScale(1f, buttonInDuration).SetEase(buttonEase));
        winSequence.Join(exitButton.DOScale(1f, buttonInDuration).SetEase(buttonEase)); // 'Join' để chạy cùng lúc với restartButton
        winSequence.Play();
    }
    [ContextMenu("Test Show Win Panel")]
    private void TestShow()
    {
        ShowWinPanel();
    }
}