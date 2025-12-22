﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CONSTANT
{

    public static class PoolName
    {
        
        
    }
    
    public static class SceneName
    {
        public static string bootScene = "Boot";
        public static string bufferScene = "Buffer";
        public static string inGameScene = "InGame";
    }


    public static class StateName
    {
        public static readonly string State = "State";
        public static readonly string Action = "Action";
        
    }

    public static class StateTurret
    {
        public static string Idle = "idle";
        public static string Attack = "attack";
    }
    public static class PathName
    {
        public static readonly string TurretPath = "TurretConfigs";
        public static readonly string LevelPath = "LevelConfigs";
    }

    public static class BulletName
    {
        public static readonly string BallistaBullet = "BallistaBullet";
    }

}