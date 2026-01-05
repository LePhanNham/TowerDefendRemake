
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TurretInformation : MonoBehaviour
{
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private Button CloseBtn;
    [SerializeField] private TurretBase turretbase;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        upgradeBtn.onClick.AddListener(() => turretbase.UpgradeLevel("Level Max Updated",OnUpgradeSuccess));
        sellBtn.onClick.AddListener(() => turretbase.SellTurretBase());
        CloseBtn.onClick.AddListener(FadeOutClose);
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    void OnUpgradeSuccess()
    {
        Debug.Log("Upgrade thành công");
    }

    public void FadeInOpen()
    {
        canvasGroup.DOFade(1, 0.5f);
    }
    public void FadeOutClose()
    {
        canvasGroup.DOFade(0, 0.5f);
    }
}
