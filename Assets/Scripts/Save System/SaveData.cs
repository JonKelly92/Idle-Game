using System.Collections.Generic;
using System.Collections;
using System;

[Serializable]
public class SaveData
{
    public double CurrencyTier1;
    public DateTime LastTimerTick;
    public List<FactoryData> Factories;

    public SaveData()
    {
        Factories = new List<FactoryData>();
    }
}
