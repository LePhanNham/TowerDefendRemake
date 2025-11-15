
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HpHubPoolData : IPoolableData
{
    public readonly Transform AnchorHub;
    public readonly RectTransform Parent;
    public float BeforeHp { get;private set; }
    public float CurrentHp { get;private set; }
    public float MaxHp { get;private set; }

    public HpHubPoolData(Transform anchorHub, RectTransform parent)
    {
        this.AnchorHub = anchorHub;
        this.Parent = parent;
    }

    public void SetValue(float hp,float currentHp,float maxHp)
    {
        this.BeforeHp = hp;
        this.CurrentHp = currentHp;
        this.MaxHp = maxHp;
    }
}
[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class HpHub : PoolableObject
{
    [SerializeField] private Image hpImage;
    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Camera mainCamera;
    
    private RectTransform parentCanvasRect;
    private Transform anchorHub;
    private Tween twHp;
    private Tween twFade;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        mainCamera = Camera.main; 
    }

    private HpHubPoolData data;
    public override void OnSpawn(IPoolableData newData)
    {
        base.OnSpawn(newData);
        gameObject.SetActive(true);
        data = (HpHubPoolData)newData;
        anchorHub = data.AnchorHub;
        parentCanvasRect = data.Parent;
        transform.SetParent(parentCanvasRect, false);
        SetHudPosition();
        UpdateHp(data.BeforeHp, data.CurrentHp, data.MaxHp);
    }
    
    public override void OnDespawn()
    {
        base.OnDespawn();
        twHp?.Kill();
        twFade?.Kill();
        parentCanvasRect = null;
        anchorHub = null;
        gameObject.SetActive(false);
    }
    
    private void UpdateHp(float beforeHp, float cur, float max)
    {
        canvasGroup.alpha = 1;
        hpImage.fillAmount = beforeHp / max;
        float targetFill = cur / max;
        
        twHp?.Kill();
        twHp = hpImage.DOFillAmount(targetFill, 0.5f).SetEase(Ease.OutCubic);
        
        twFade?.Kill();
        twFade = canvasGroup.DOFade(0, 0.5f)
            .SetDelay(1f) 
            .OnComplete(() => 
            {
                PoolManager.Instance.ReleaseToPool("HpHub", this);
            });
    }

    private Vector2 screenPoint;
    private Vector2 localPoint;
    private void SetHudPosition()
    {
        if (parentCanvasRect == null || mainCamera == null || anchorHub == null)
        {
            return;
        }
        screenPoint = mainCamera.WorldToScreenPoint(anchorHub.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvasRect, 
            screenPoint, 
            null, 
            out localPoint
        );
        rect.anchoredPosition = localPoint;
    }

    private void Update()
    {
        SetHudPosition();
    }
}
