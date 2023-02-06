using System.Collections.Generic;
using System.Collections;
using System;

[Serializable]
public class SaveData
{
    public double CurrencyTier1;
    public long LastTimerTick; // we store this in ticks because DateTime cannot be serialized
    public List<FactoryData> Factories;

    public SaveData()
    {
        Factories = new List<FactoryData>();
    }
}
