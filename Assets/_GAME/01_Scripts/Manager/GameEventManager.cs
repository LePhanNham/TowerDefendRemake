
using System;
using UnityEngine;

public static class GameEventManager
{
    public static event Action<NodeBase> onBuildTurretCompleted;
    public static event Action<NodeBase> onTurretSold;

    public static event Action<string> onLevelMaxUpdated;
    
    public static event Action<int> onAddMoneyUpdated;
    public static event Action<int> onUseMoneyUpdated;

    public static event Action<string> onShowUnableToBuy; 
    public static event Action<string> onShowUnableToUpgrade; 
    
    public static event Action onUpdatedEnemiesDie;
    public static event Action<string> onNotifyCurrentWave;    
    public static event Action<int> onBaseHpUpdated;
    public static event Action onBaseHpZero;
    public static void BuildTurretCompleted(NodeBase obj)
    {
        onBuildTurretCompleted?.Invoke(obj);
    }

    public static void TurretSold(NodeBase obj)
    {
        onTurretSold?.Invoke(obj);
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

    public static void UpdatedEnemiesDie()
    {
        onUpdatedEnemiesDie?.Invoke();
    }

    public static void BaseHpUpdated(int hp)
    {
        onBaseHpUpdated?.Invoke(hp);
    }

    public static void BaseHpZero()
    {
        onBaseHpZero?.Invoke();
    }

    public static void ShowUnableToUpgrade(string obj)
    {
        onShowUnableToUpgrade?.Invoke(obj);
    }

    public static void LevelMaxUpdated(string obj)
    {
        onLevelMaxUpdated?.Invoke(obj);
    }

    public static void NotifyCurrentWave(string obj)
    {
        onNotifyCurrentWave?.Invoke(obj);
    }
}
