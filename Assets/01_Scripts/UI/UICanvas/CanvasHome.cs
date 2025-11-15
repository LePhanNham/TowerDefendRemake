using UnityEngine;
using UnityEngine.UI;
using CONSTANT;
public class CanvasHome : UICanvas
{
    [SerializeField] private Button startGamePlay;

    protected override void Awake()
    {
        base.Awake();
        startGamePlay.onClick.AddListener(OnClickStartButton);
    }

    private void OnClickStartButton()
    {
        CloseDirectly();
        UIManager.Instance.OpenUI<CanvasInitialize>();
        // LoadSceneManager.Instance.LoadSceneByName(SceneName.inGameScene, () =>
        // {
        //     UIManager.Instance.CloseAll();
        //     UIManager.Instance.OpenUI<CanvasGamePlay>();
        // });
    }
}
