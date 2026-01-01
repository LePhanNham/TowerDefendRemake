
using System;
using UnityEngine;

public static class GameEventManager
{
    public static event Action<NodeBase> onBuildTurretCompleted;

    [Header("UI Event")] public static Action<string> OnLevelMaxUpdated;
    
    public static event Action<int> onAddMoneyUpdated;
    public static event Action<int> onUseMoneyUpdated;

    public static event Action<string> onShowUnableToBuy; 
    

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
}
