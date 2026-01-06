
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretAction : MonoBehaviour
{
    public void OnMouseDown()
    {
        if (EnemyManager.Instance.EnemyCountInWave() != 0) return;
        var turretBase = GetComponent<TurretBase>();
        if (turretBase == null) return;
        if (TurretInformation.Instance != null)
        {
            TurretInformation.Instance.Show(turretBase);
        }
        TutorialManager.Instance.ReportAction(TutorialActionType.ShowTurretPopup);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EnemyManager.Instance.EnemyCountInWave() != 0) return;
            if (Camera.main == null) return;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider);
                var tb = hit.collider.GetComponentInParent<TurretBase>();
                if (tb != null && TurretInformation.Instance != null)
                {
                    TurretInformation.Instance.Show(tb);
                    TutorialManager.Instance.ReportAction(TutorialActionType.ShowTurretPopup);
                }
            }
        }
    }
    
}


