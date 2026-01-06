
using System;
using UnityEngine;

public static class GameEventManager
{
    public static event Action<NodeBase> onBuildTurretCompleted;

    public static event Action<string> onLevelMaxUpdated;
    
    public static event Action<int> onAddMoneyUpdated;
    public static event Action<int> onUseMoneyUpdated;

    public static event Action<string> onShowUnableToBuy; 
    public static event Action<string> onShowUnableToUpgrade; 
    
    public static event Action<TurretBase> onUpgradeTurret;
    public static event Action<TurretBase> onSellTurret;
    public static event Action onUpdatedEnemiesDie;
    
    public static void BuildTurretCompleted(NodeBase obj)
    {
        onBuildTurretCompleted?.Invoke(obj);
    }

    public static void AddMoneyUpdated(int money)
    {
        onAddMoneyUpdated?.Invoke(money);
    }

    public static void UseMoneyUpdated(int money)
    {
        onUseMoneyUpdated?.Invoke(money);
    }

    public static void ShowUnableToBuy(string msg)
    {
        onShowUnableToBuy?.Invoke(msg);
    }

    public static void UpgradeTurret(TurretBase obj)
    {
        onUpgradeTurret?.Invoke(obj);
    }

    public static void SellTurret(TurretBase obj)
    {
        onSellTurret?.Invoke(obj);
    }

    public static void UpdatedEnemiesDie()
    {
        onUpdatedEnemiesDie?.Invoke();
    }

    public static void ShowUnableToUpgrade(string obj)
    {
        onShowUnableToUpgrade?.Invoke(obj);
    }

    public static void LevelMaxUpdated(string obj)
    {
        onLevelMaxUpdated?.Invoke(obj);
    }
}
