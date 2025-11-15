using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform hubAnchor;
    public RectTransform HUDAnchor { get => hubAnchor; set => hubAnchor = value; }
}