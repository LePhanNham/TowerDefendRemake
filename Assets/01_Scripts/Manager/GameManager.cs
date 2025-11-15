
using System;
using UnityEngine;
public enum GameState { MainMenu, GamePlay, Finish, Revive, Setting }
public class GameManager : SingletonMono<GameManager>
{
    private static GameState gameState;
    public static GameState GameState => gameState;

    public void Awake()
    {
        // Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * maxScreenHeight),maxScreenHeight,true);
        }
    }

    public static void ChangeState(GameState gameState)
    {
        gameState = gameState;
    }
    
    public static bool IsState(GameState state) => gameState == state;
}

