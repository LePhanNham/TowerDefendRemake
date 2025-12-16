
using System;
using System.Collections.Generic;
using System.Linq;
using _01_Scripts.Data.Level;
using UnityEngine;

public class ConfigManager : SingletonMono<ConfigManager>
{
    [SerializeField] private List<TurretConfig> turretConfigs;
    [SerializeField] private List<LevelConfig> levelConfigs;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        turretConfigs = Resources.LoadAll<TurretConfig>(CONSTANT.PathName.turretPath).ToList();
        levelConfigs = Resources.LoadAll<LevelConfig>(CONSTANT.PathName.levelPath).ToList();
    }

    public List<TurretConfig> GetTurretConfigs()
    {
        return turretConfigs;
    }

    // public void LoadAllTurretCard()
    // {
    //     foreach (TurretConfig config in turretConfigs)
    //     {
    //         config.
    //     }
    // }
    public List<LevelConfig> GetLevelConfigs()
    {
        return levelConfigs;
    }
}
