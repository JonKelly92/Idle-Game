using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/BoolVariable")]
public class BoolVariable : DescriptionBaseSO
{
    public UnityEvent<bool> OnValueChanged { get; private set; } = new UnityEvent<bool>();

    [SerializeField] private bool _value;

    public bool Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}
