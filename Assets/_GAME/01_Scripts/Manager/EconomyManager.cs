
using System;
using _01_Scripts.Data.Level;

public class EconomyManager : SingletonMono<EconomyManager>
{
    private int currentEconomy;
    public int CurrentEconomy => currentEconomy;
    private bool isFirstTime = true;

    public void Init(LevelConfig levelConfig)
    {
        if (isFirstTime) currentEconomy = 0;
        AddMoney(levelConfig.CostBase);
    }

    public void OnEnable()
    {
        GameEventManager.onAddMoneyUpdated += AddMoney;
        GameEventManager.onUseMoneyUpdated += UseMoney;
        
    }

    public void OnDisable()
    {
        GameEventManager.onAddMoneyUpdated -= AddMoney;
        GameEventManager.onUseMoneyUpdated -= UseMoney;
    }

    public void AddMoney(int money)
    {
        currentEconomy += money;
    }

    public void UseMoney(int money)
    {
        currentEconomy -= money;
        if (currentEconomy <= 0) currentEconomy = 0;
    }
}
