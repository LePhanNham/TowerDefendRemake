using UnityEngine;
using UnityEngine.UI;

public class CanvasHome : UICanvas
{
    [SerializeField] private HomePanelAnimator homePanelAnimator;
    [SerializeField] private Button  startGameButton, exitButton;

    [Header("Map / Level")]
    [SerializeField] private MapData defaultMap;
    [SerializeField] private Text levelNameText;

    protected override void Awake()
    {
        base.Awake();
        startGameButton.onClick.AddListener(OnClickStartGameButton);
        // settingsButton.onClick.AddListener(OnClickSettingsButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        if (levelNameText != null)
            levelNameText.text = defaultMap != null ? defaultMap.mapName : "Level";
    }

    public override void Open()
    {
        base.Open();
        homePanelAnimator.ShowHomePanel();
    }

    public override void Close(float t)
    {
        base.Close(t);
        homePanelAnimator.HideHomePanel();
    }

    private void OnClickStartGameButton()
    {
        // Delegate map loading to MapManager (map logic not owned by UI)
        if (MapManager.Instance != null)
        {
            MapManager.Instance.LoadMap(LevelManager.Instance.LevelConfig.MapData);
        }

        Close(0f);
        LevelManager.Instance.StartLevel();
    }

    // private void OnClickSettingsButton()
    // {
    //     UIManager.Instance.OpenUI<CanvasSetting>();
    // }

    private void OnClickExitButton()
    {
        Application.Quit();
    }
}