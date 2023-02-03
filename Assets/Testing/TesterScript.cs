using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterScript : MonoBehaviour
{
    [SerializeField] public double DefaultAmountOfCurrency_Tier1;
    [SerializeField] public PlayerCurrencyManagerScriptableObject PlayerCurrenyManagerSO;
    [SerializeField] public LastTimerUpdateScriptableObject LastTimerUpdate;
    [SerializeField] public List<FactoryValuesScriptableObject> Factories = new List<FactoryValuesScriptableObject>();

    public void ResetFactoryValues()
    {
        Debug.Log("Factory Values Reset");

        foreach (var factory in Factories)
        {
            factory.LevelSO.Value = 0;
            factory.PayoutAmountSO.Value = factory.BasePayoutAmount;
            factory.UpgradeCostSO.Value = factory.BaseUpgradeCost;
            factory.PayoutTimeRemainingSO.Value = factory.TimeBetweenPayouts;
        }
    }

    public void ResetCurrency()
    {
        Debug.Log("Currency Reset");

        PlayerCurrenyManagerSO.SpendTier1Currency(PlayerCurrenyManagerSO.CurrencyTier1.Value);
        PlayerCurrenyManagerSO.AddTier1Currency(DefaultAmountOfCurrency_Tier1);
    }

    public void ResetTimer()
    {
        Debug.Log("Timer Reset");

        LastTimerUpdate.Value = DateTime.Now;
    }

    public void ResetEverything()
    {
        ResetFactoryValues();
        ResetCurrency();
        ResetTimer();
    }
}
