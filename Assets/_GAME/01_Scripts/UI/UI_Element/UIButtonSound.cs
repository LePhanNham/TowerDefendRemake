using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private SoundManager.SoundId sound = SoundManager.SoundId.Click;
    [SerializeField] private float volume = 1f;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (btn != null)
            btn.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        if (btn != null)
            btn.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.Play(sound, volume);
    }
}
