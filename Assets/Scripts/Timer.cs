using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /*
     
        What do I need it to do?

            - factories need to recieve and event every x amount of time
                - the event will pass seconds
            
            - this event needs to pass in the amount of time that has passed since the last event that was sent
                - the factory will then be able to tell if the end of the pay period has been reached
                - and how many times it has been reached

            - whenever the event is sent also store DateTime.Now in a SO
                - use this SO to determine how much time has passed the next time we send the event
     
     */

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
