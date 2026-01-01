
using System;
using UnityEngine;
using UnityEngine.UI;

public class TurretPanel : MonoBehaviour
{
    [SerializeField] private Button btnSell;
    [SerializeField] private Button btnUpgrade;

    private void Awake()
    {
        btnSell.onClick.AddListener(SellTurret);
        btnUpgrade.onClick.AddListener(UpgradeTurret);
    }

    private void SellTurret()
    {
        
    }

    private void UpgradeTurret()
    {
        
    }
}
