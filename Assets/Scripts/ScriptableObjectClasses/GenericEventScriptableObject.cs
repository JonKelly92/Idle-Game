using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/GenericEvent")]
public class GenericEventScriptableObject : DescriptionBaseSO
{
    public UnityAction OnEventRaised;

    public void SendEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}
