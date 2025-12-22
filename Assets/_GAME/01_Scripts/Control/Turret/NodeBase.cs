
using System;
using System.Collections.Generic;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    private Vector3 buildPosition;
    public Vector3 BuildPosition => buildPosition;
    private void Awake()
    {
        buildPosition = transform.position;
    }
    private void OnMouseDown()
    {
        TurretCardPanel.Instance.Show(this);
    }

    private void OnEnable()
    {
        GameEventManager.OnBuildTurretCompleted += OnHide;
    }

    private void OnDisable()
    {
        GameEventManager.OnBuildTurretCompleted -= OnHide;
    }
    private void OnHide(NodeBase obj)
    {
        if (obj != this) return;
        gameObject.SetActive(false);
    }
}
