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
        public static readonly string TankBullet = "TankBullet";
    }

    public static class Message
    {
        public static readonly string Success = "Success";
        public static readonly string Fail = "Fail";
        public static readonly string UnableToBuy = "Don't enough money";
        
    }

    public static class TutorialMessage
    {
        public static readonly string step_1 = "StartNode";
        public static readonly string step_2 = "FireTurret";
        public static readonly string step_3 = "StartBtn";
        public static readonly string step_4 = "Enemy";
        public static readonly string step_5 = "TurretInformation";
        public static readonly string step_6 = "Upgrade";
        public static readonly string step_7 = "Sell";
    }

    public static class EffectName
    {
        public static readonly string TankExplosion = "TankExplosionEffect";
    }
}