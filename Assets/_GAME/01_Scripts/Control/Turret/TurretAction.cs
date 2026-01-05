
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretAction : MonoBehaviour
{
    [SerializeField] private TurretInformation panel;

    public void OnMouseDown()
    {
        if (EnemyManager.Instance.EnemyCountInWave()==0)
        {
            panel.FadeInOpen();
            TutorialManager.Instance.ReportAction(TutorialActionType.ShowTurretPopup);
        }
    }
}


