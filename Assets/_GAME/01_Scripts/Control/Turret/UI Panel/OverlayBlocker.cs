using Unity.VisualScripting;
using UnityEngine;

public class OverlayBlocker : MonoBehaviour
{
    public void OnClick()
    {
        TurretCardPanel.Instance.Hide();
        gameObject.SetActive(false);
    }
}
