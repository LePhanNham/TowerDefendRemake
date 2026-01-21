
using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TurretInformation : SingletonMono<TurretInformation>
{
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button sellBtn;
    [FormerlySerializedAs("CloseBtn")] [SerializeField] private Button closeBtn;
    private TurretBase turretbase;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI range;
    [SerializeField] private TextMeshProUGUI fireCooldown;
    private CanvasGroup canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        closeBtn.onClick.AddListener(FadeOutClose);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameEventManager.onTurretSold += HandleTurretSold;
    }

    private void OnDisable()
    {
        GameEventManager.onTurretSold -= HandleTurretSold;
    }

    private void HandleTurretSold(NodeBase node)
    {
        // if the currently shown turret was on this node, close the panel
        Debug.Log($"TurretInformation.HandleTurretSold: node={node?.name}, currentTurretHost={(turretbase!=null? turretbase.HostNode?.name : "null")}");
        if (turretbase != null && turretbase.HostNode == node)
        {
            FadeOutClose();
        }
    }

    public void Show(TurretBase target)
    {
        turretbase = target;
        // remove previous listeners and rebind to current target
        upgradeBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.AddListener(() => turretbase.UpgradeLevel("Level Max Updated", OnUpgradeSuccess));
        sellBtn.onClick.AddListener(() => turretbase.SellTurretBase());
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(FadeOutClose);
        UpdateUI(turretbase.GetCurrentTurretLevel());
        // bring panel to front so its interactive elements are above other canvas UI
        var rt = GetComponent<RectTransform>();
        if (rt != null) rt.SetAsLastSibling();
        FadeInOpen();
    }
    public void UpdateUI(TurretLevel config)
    {
        level.text = "Level " + config.level.ToString();
        damage.text = config.damage.ToString();
        cost.text = config.cost.ToString();
        range.text = config.range.ToString();
        fireCooldown.text = config.fireRate.ToString();
    }
    void OnUpgradeSuccess()
    {
        Debug.Log("Upgrade thành công");
    }

    public void FadeInOpen()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasGroup.DOFade(1, 0.5f);
    }

    // Expose whether the information panel is currently open and blocking input.
    // Also check alpha/interactable to avoid treating a hidden-but-active canvas as open.
    public bool IsOpen => canvasGroup != null
                            && canvasGroup.gameObject.activeInHierarchy
                            && canvasGroup.blocksRaycasts
                            && canvasGroup.interactable
                            && canvasGroup.alpha > 0.05f;

    public void FadeOutClose()
    {
        canvasGroup.DOFade(0, 0.5f)
            .OnStart(() =>
            {
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            })
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
