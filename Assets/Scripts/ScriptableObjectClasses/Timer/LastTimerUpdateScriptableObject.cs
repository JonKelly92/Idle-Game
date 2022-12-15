using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LastTimerUpdate", menuName = "ScriptableObjects/LastTimerUpdate")]
public class LastTimerUpdateScriptableObject : DescriptionBaseSO
{
    private long _ticks;

    public DateTime Value 
    { 
        get { return new DateTime(_ticks); }
        set { _ticks = value.Ticks; }
    }

}
