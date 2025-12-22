
using System;
using UnityEngine;

public static class GameEventManager
{
    public static event Action<NodeBase> OnBuildTurretCompleted;



    [Header("UI Event")] public static Action<string> OnLevelMaxUpdated;

    public static void OnOnBuildTurretCompleted(NodeBase obj)
    {
        OnBuildTurretCompleted?.Invoke(obj);
    }
}
