
using System;
using System.Collections.Generic;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    private Vector3 buildPosition;
    private TutorialTarget tutorialTarget;
    public Vector3 BuildPosition => buildPosition;
    private void Awake()
    {
        buildPosition = transform.position;
        tutorialTarget = GetComponent<TutorialTarget>();
    }

    private void Start()
    {
        tutorialTarget.SetID(CONSTANT.TutorialMessage.step_1);
    }

    private void OnMouseDown()
    {
        TurretCardPanel.Instance.Show(this);
        TutorialManager.Instance.ReportAction(TutorialActionType.SelectNode);
    }

    private void OnEnable()
    {
        GameEventManager.onBuildTurretCompleted += OnHide;
    }

    private void OnDisable()
    {
        GameEventManager.onBuildTurretCompleted -= OnHide;
    }
    private void OnHide(NodeBase obj)
    {
        if (obj != this) return;
        gameObject.SetActive(false);
    }
}
