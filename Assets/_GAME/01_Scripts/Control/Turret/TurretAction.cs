
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretAction : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void OnMouseDown()
    {
        panel.SetActive(true);
    }
}


