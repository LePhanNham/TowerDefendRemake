
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        turretConfigs = Resources.LoadAll<TurretConfig>(CONSTANT.PathName.TurretPath).ToList();
        levelConfigs = Resources.LoadAll<LevelConfig>(CONSTANT.PathName.LevelPath).ToList();
    }


    public List<TurretConfig> GetTurretConfigs()
    {
        return turretConfigs;
    }


    public List<LevelConfig> GetLevelConfigs()
    {
        return levelConfigs;
    }
}
