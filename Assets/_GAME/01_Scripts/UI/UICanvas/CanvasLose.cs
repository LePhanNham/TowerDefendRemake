using UnityEngine;
using UnityEngine.UI;

public class CanvasLose : UICanvas
{
    [SerializeField] private LosePanelSimpleAnimator losePanelSimpleAnimator;
    [SerializeField] private Button  restartGameButton, closeButton;
    protected override void Awake()
    {
        base.Awake();
        restartGameButton.onClick.AddListener(OnClickRestartGameButton);
        closeButton.onClick.AddListener(OnClickCloseButton);
    }
    public override void Open()
    {
        base.Open();
        losePanelSimpleAnimator.ShowLosePanel();
    }

    public override void Close(float t)
    {
        losePanelSimpleAnimator.HideLosePanel();
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


    private void OnClickCloseButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasHome>();
    }
}