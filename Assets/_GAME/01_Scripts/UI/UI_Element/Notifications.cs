
using System;
using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventManager.OnLevelMaxUpdated += AnnounceLevelMax;
    }

    private void OnDisable()
    {
        GameEventManager.OnLevelMaxUpdated -= AnnounceLevelMax;
    }

    private void AnnounceLevelMax(string notice)
    {
        NotificateInformation(notice);
    }
    private void NotificateInformation(string message)
    {
        Debug.Log(message);
    }
}
