
using System.Collections.Generic;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    private Vector3 buildPosition;
    private void Awake()
    {
        buildPosition = transform.position;
    }
    private void OnMouseDown()
    {
        TurretCardPanel.Instance.Show(buildPosition);
    }
}
