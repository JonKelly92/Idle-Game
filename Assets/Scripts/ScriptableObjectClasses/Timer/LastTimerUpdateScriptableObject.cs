using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LastTimerUpdate", menuName = "ScriptableObjects/LastTimerUpdate")]
public class LastTimerUpdateScriptableObject : DescriptionBaseSO
{
    private long _ticks;

    [field: SerializeField] public string Id { get; private set; }
    [SerializeField] private string DateAndTime; // for debugging

    public DateTime Value
    {
        get { return new DateTime(_ticks); }
        set
        {
            _ticks = value.Ticks;

            DateAndTime = new DateTime(_ticks).ToString();
        }
    }
}
