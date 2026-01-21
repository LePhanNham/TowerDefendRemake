
using System;
using System.Collections.Generic;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeBase : MonoBehaviour
{
    private Vector3 buildPosition;
    private TutorialTarget tutorialTarget;
    private TurretBase hostTurret;
    public Vector3 BuildPosition => buildPosition;
    public TurretBase HostTurret => hostTurret;
    private void Awake()
    {
        var p = transform.position;
        buildPosition = new Vector3(p.x, p.y, 0f); // ensure build position Z = 0 for 2D
        tutorialTarget = GetComponent<TutorialTarget>();
        // Subscribe here so node receives events even when deactivated
        GameEventManager.onBuildTurretCompleted += OnHide;
        GameEventManager.onTurretSold += OnShow;
    }

    private void Start()
    {
        tutorialTarget.SetID(CONSTANT.TutorialMessage.step_1);
    }

    private void OnMouseDown()
    {
        // If TurretInformation panel is open, block building on nodes
        if (TurretInformation.Instance != null && TurretInformation.Instance.IsOpen)
            return;

        TurretCardPanel.Instance.Show(this);
        TutorialManager.Instance.ReportAction(TutorialActionType.SelectNode);
    }

    private void OnDestroy()
    {
        GameEventManager.onBuildTurretCompleted -= OnHide;
        GameEventManager.onTurretSold -= OnShow;
    }
    private void OnHide(NodeBase obj)
    {
        if (obj != this) return;
        gameObject.SetActive(false);
    }
    private void OnShow(NodeBase obj)
    {
        if (obj != this) return;
        Debug.Log($"NodeBase.OnShow: showing node {name}");
        gameObject.SetActive(true);
        // clear occupied turret reference when node is shown again
        hostTurret = null;
    }

    // mark this node occupied by a turret and hide node visuals
    public void Occupy(TurretBase turret)
    {
        hostTurret = turret;
        gameObject.SetActive(false);
    }

}
    