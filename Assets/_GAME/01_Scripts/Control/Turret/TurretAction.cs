
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretAction : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void OnMouseDown()
    {
        if (EnemyManager.Instance.EnemyCountInWave()==0) panel.SetActive(true);
    }
}


