using DG.Tweening;
// using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInitialize : UICanvas
{
    [SerializeField] private Slider loadingValueSlider;
    // [SerializeField] private TextMeshProUGUI loadingValueText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button startButton;
    // protected override void Awake()
    // {
    //     base.Awake();
    //     startButton.onClick.AddListener(OnClickStartButton);
    // }

    // public override void Open()
    // {
    //     base.Open();
    //     LoadSceneManager.Instance.OnUpdateProgressEvent += UpdateLoadingUI;
    //     if (canvasGroup == null) return;
    //     startButton.gameObject.SetActive(false);
    //     loadingValueSlider.gameObject.SetActive(true);
    //     canvasGroup.alpha = 0;
    //     canvasGroup.DOFade(1, 1).SetLink(gameObject).OnComplete(() =>
    //     {
    //         canvasGroup.alpha = 1;
    //     });
    // }
    // private void UpdateLoadingUI(float progressValue)
    // {
    //     if (loadingValueSlider == null || loadingValueText == null) return;
    //
    //     loadingValueSlider.value = progressValue;
    //     loadingValueText.text = $"Loading... {(int)progressValue}%";
    //     if (progressValue >= 100f)
    //     {
    //         startButton.gameObject.SetActive(true);
    //         loadingValueSlider.gameObject.SetActive(false);
    //     }
    // }
    //
    // private void OnClickStartButton()
    // {
    //     UIManager.Instance.OpenUI<CanvasHome>();
    //     //Debug.Log("Start Button clicked");
    //     Close(0f);
    // }
    //
    // public override void Close(float time)
    // {
    //     LoadSceneManager.Instance.OnUpdateProgressEvent -= UpdateLoadingUI;
    //     base.Close(time);
    // }
}
