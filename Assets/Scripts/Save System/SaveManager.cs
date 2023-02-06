using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string SAVE_DATA_ID = "save_data_id";

    public float AutoSaveFrequncy;
    [SerializeField] public PlayerCurrencyManagerScriptableObject PlayerCurrenyManagerSO;
    [SerializeField] public LastTimerUpdateScriptableObject LastTimerUpdate;
    [SerializeField] public List<FactoryValuesScriptableObject> Factories = new List<FactoryValuesScriptableObject>();

    private SaveData _saveData;

    void Start()
    {
        Debug.Log(" START ");

        _saveData = new SaveData();

        UpdateSaveData();// we do this to set default values that are already in the scriptable objects

        var data = SaveSystem.Load(_saveData, SAVE_DATA_ID);

        ApplyLoadedData(data);

        InvokeRepeating("Save", AutoSaveFrequncy, AutoSaveFrequncy);
    }

    private void Save()
    {
        Debug.Log(" SAVE ");

        UpdateSaveData();
        SaveSystem.Save(_saveData, SAVE_DATA_ID);
    }

    private void UpdateSaveData()
    {
        _saveData.Factories.Clear();

        for (int i = 0; i < Factories.Count; i++)
        {
            _saveData.Factories.Add(new FactoryData());
            _saveData.Factories[i].SetData(Factories[i].LevelSO.Value, Factories[i].PayoutAmountSO.Value, Factories[i].UpgradeCostSO.Value);
        }

        _saveData.CurrencyTier1 = PlayerCurrenyManagerSO.CurrencyTier1.Value;
        _saveData.LastTimerTick = LastTimerUpdate.Value.Ticks;
    }

    private void ApplyLoadedData(SaveData data)
    {
        for (int i = 0; i < Factories.Count; i++)
        {
            Factories[i].LevelSO.Value = data.Factories[i].Level;
            Factories[i].PayoutAmountSO.Value = data.Factories[i].PayoutAmount;
            Factories[i].UpgradeCostSO.Value = data.Factories[i].UpgradeCost;
        }

        PlayerCurrenyManagerSO.CurrencyTier1.Value = data.CurrencyTier1;
        LastTimerUpdate.Value = new DateTime(data.LastTimerTick);
    }
}