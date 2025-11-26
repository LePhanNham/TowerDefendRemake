
using System;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    // [SerializeField] 
    private Vector3 position;

    private void Awake()
    {
        position = transform.position;
    }
}
