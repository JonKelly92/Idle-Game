using BreakInfinity;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TimerTickEvent", menuName = "ScriptableObjects/TimerTickEvent")]
public class TimerTickEvent : DescriptionBaseSO
{
    public UnityEvent<BigDouble> OnTimerTick { get; private set; } = new UnityEvent<BigDouble>();

    /// <summary>
    /// Pass in the time that has passed since the last timer tick then it sends an event
    /// </summary>
    /// <param name="timeThatPassed"></param>
    public void SendEvent(BigDouble timeThatPassed)
    {
        if (OnTimerTick != null)
            OnTimerTick.Invoke(timeThatPassed);
    }
}
