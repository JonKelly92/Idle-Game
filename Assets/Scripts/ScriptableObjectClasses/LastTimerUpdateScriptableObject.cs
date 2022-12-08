using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LastTimerUpdate", menuName = "ScriptableObjects/LastTimerUpdate")]
public class LastTimerUpdateScriptableObject : DescriptionBaseSO
{
    public DateTime LastUpdate { get; private set; }

    public void TimerWasUpdated(DateTime lastTimerUpdate)
    {
        // TODO : check if the new datetime is after the existing datetime

        LastUpdate = lastTimerUpdate;
    }

}
