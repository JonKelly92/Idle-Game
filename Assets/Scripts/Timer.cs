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
        int isNewGame = PlayerPrefs.GetInt("NewGame", 0);

        // If this is the first time that the game is being run we need to initialize _lastTimerUpdate.Value
        if(isNewGame == 0)
        {
            PlayerPrefs.SetInt("NewGame", 1);
            _lastTimerUpdate.Value = DateTime.Now;
        }

        InvokeRepeating("TimerTick", _timeBetweenTicks, _timeBetweenTicks);
    }

    private void TimerTick()
    {
        _timeThatPassed = DateTime.Now - _lastTimerUpdate.Value;

        _timerTickEvent.SendEvent(_timeThatPassed.TotalMilliseconds);

        _lastTimerUpdate.Value = DateTime.Now;
    }
}
