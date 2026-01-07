
using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textNotification;
    private Tween notifyTween;
    private void OnEnable()
    {
        GameEventManager.onLevelMaxUpdated += AnnounceLevelMax;
        GameEventManager.onShowUnableToBuy += ShowUnableToBuy;
        GameEventManager.onShowUnableToUpgrade += ShowUnableToUpgrade;
        GameEventManager.onNotifyCurrentWave += ShowNoticeWave;

    }

    private void OnDisable()
    {
        GameEventManager.onLevelMaxUpdated -= AnnounceLevelMax;
        GameEventManager.onShowUnableToBuy -= ShowUnableToBuy;
        GameEventManager.onShowUnableToUpgrade -= ShowUnableToUpgrade;
        GameEventManager.onNotifyCurrentWave -= ShowNoticeWave;
    }

    private void AnnounceLevelMax(string notice)
    {
        NotificateInformation(notice);
    }
    private void ShowUnableToBuy(string notice)
    {
        NotificateInformation(notice);
    }
    private void ShowUnableToUpgrade(string notice)
    {
        NotificateInformation(notice);
    }

    private void ShowNoticeWave(string notice)
    {
        NotificateInformation(notice);
    }
    private void NotificateInformation(string message)
    {
        textNotification.text = message;

        notifyTween?.Kill();

        Vector3 startPos = textNotification.rectTransform.anchoredPosition;
        textNotification.rectTransform.anchoredPosition = startPos + Vector3.down * 20;

        textNotification.alpha = 0;
        textNotification.gameObject.SetActive(true);

        notifyTween = DOTween.Sequence()
            .Append(textNotification.DOFade(1f, 0.3f))
            .Join(textNotification.rectTransform.DOAnchorPos(startPos, 0.3f))
            .AppendInterval(1.2f)
            .Append(textNotification.DOFade(0f, 0.3f))
            .OnComplete(() =>
            {
                textNotification.gameObject.SetActive(false);
            });
    }
}
