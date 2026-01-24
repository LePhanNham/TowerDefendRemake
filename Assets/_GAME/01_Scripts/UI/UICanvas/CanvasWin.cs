using UnityEngine;
using UnityEngine.UI;

public class CanvasWin : UICanvas
{
    [SerializeField] private WinPanelAnimator winPanelAnimator;
    [SerializeField] private Button restartGameButton, nextButton, closeButton;
    protected override void Awake()
    {
        base.Awake();
        restartGameButton.onClick.AddListener(OnClickRestartGameButton);
        nextButton.onClick.AddListener(OnClickNextButton);
        closeButton.onClick.AddListener(OnClickCloseButton);
    }
    public override void Open()
    {
        base.Open();
        winPanelAnimator.ShowWinPanel();
    }

    public override void Close(float t)
    {
        base.Close(t);
    }
    
    private void OnClickRestartGameButton()
    {
        Close(0f);
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.StartLevel();
        }
    }

    private void OnClickNextButton()
    {
        Close(0f);
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.NextLevel();
        }
    }

    private void OnClickCloseButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasHome>();
    }
}