using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    

    [SerializeField] private float _timeBetweenTicks;

    [SerializeField] private TimerTickEvent _timerTickEvent;
    [SerializeField] private LastTimerUpdateScriptableObject _lastTimerUpdate;

    private TimeSpan _timeThatPassed;
 
    private void Start()
    {
        InvokeRepeating("TimerTick", _timeBetweenTicks, _timeBetweenTicks);
    }

    private void TimerTick()
    {
        _timeThatPassed = DateTime.Now - _lastTimerUpdate.Value;

        _timerTickEvent.SendEvent(_timeThatPassed.TotalMilliseconds);

        _lastTimerUpdate.Value = DateTime.Now;
    }
}
