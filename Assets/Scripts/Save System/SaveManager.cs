using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public float AutoSaveFrequncy;
    [SerializeField] public PlayerCurrencyManagerScriptableObject PlayerCurrenyManagerSO;
    [SerializeField] public LastTimerUpdateScriptableObject LastTimerUpdate;
    [SerializeField] public List<FactoryValuesScriptableObject> Factories = new List<FactoryValuesScriptableObject>();

    //private SaveSystem _saveSystem;
   // private SaveData _saveData;

    void Start()
    {
        Debug.Log(" START ");

        // _saveSystem = new SaveSystem();
        // _saveData = new SaveData();

        // UpdateSaveData();
        // _saveSystem.Load(_saveData);

        var data = new FactoryData();

        foreach (var factory in Factories)
        {
            data.SetData(factory.LevelSO.Value, factory.PayoutAmountSO.Value, factory.UpgradeCostSO.Value);
            var loadedData = SaveSystem.LoadFactoryData(data, factory.Id);

            factory.LevelSO.Value = loadedData.Level;
            factory.PayoutAmountSO.Value = loadedData.PayoutAmount;
            factory.UpgradeCostSO.Value = loadedData.UpgradeCost;
        }

        PlayerCurrenyManagerSO.CurrencyTier1.Value = SaveSystem.LoadCurrencyTier1(PlayerCurrenyManagerSO.CurrencyTier1.Value, PlayerCurrenyManagerSO.Id);
        LastTimerUpdate.Value = SaveSystem.LoadTimerTick(LastTimerUpdate.Value, LastTimerUpdate.Id);

        Debug.Log(" TIME : " + LastTimerUpdate.Value.ToString());

        InvokeRepeating("Save", AutoSaveFrequncy, AutoSaveFrequncy);
    }

    private void Save()
    {
        Debug.Log(" SAVE ");

        // UpdateSaveData();
        // _saveSystem.Save(_saveData);

        var data = new FactoryData();

        foreach (var factory in Factories)
        {
            data.SetData(factory.LevelSO.Value, factory.PayoutAmountSO.Value, factory.UpgradeCostSO.Value);
            SaveSystem.SaveFactoryData(data, factory.Id);
        }

        SaveSystem.SaveCurrencyTier1(PlayerCurrenyManagerSO.CurrencyTier1.Value, PlayerCurrenyManagerSO.Id);
        SaveSystem.SaveTimerTick(LastTimerUpdate.Value, LastTimerUpdate.Id);
    }

    // private void UpdateSaveData()
    // {
    //     _saveData.Factories.Clear();

    //     for (int i = 0; i < Factories.Count; i++)
    //     {
    //         _saveData.Factories.Add(new FactoryData());
    //         _saveData.Factories[i].SetData(Factories[i].LevelSO.Value, Factories[i].PayoutAmountSO.Value, Factories[i].UpgradeCostSO.Value);
    //     }

    //     _saveData.CurrencyTier1 = PlayerCurrenyManagerSO.CurrencyTier1.Value;
    //     _saveData.LastTimerTick = LastTimerUpdate.Value;
    // }
}