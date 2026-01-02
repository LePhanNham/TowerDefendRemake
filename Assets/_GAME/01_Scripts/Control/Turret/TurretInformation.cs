
using System;
using UnityEngine;
using UnityEngine.UI;

public class TurretInformation : MonoBehaviour
{
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private TurretBase turretbase;

    private void Awake()
    {
        upgradeBtn.onClick.AddListener(() => turretbase.UpgradeLevel("Level Max Updated",OnUpgradeSuccess));
        sellBtn.onClick.AddListener(() => turretbase.SellTurretBase());
    }
    
    void OnUpgradeSuccess()
    {
        Debug.Log("Upgrade thành công");
    }

}
