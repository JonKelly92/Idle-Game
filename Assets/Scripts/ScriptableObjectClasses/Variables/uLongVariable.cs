using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/uLongVariable")]
public class uLongVariable : DescriptionBaseSO
{
    public UnityEvent<ulong> OnValueChanged { get; private set; } = new UnityEvent<ulong>();

    [SerializeField] private ulong _value;

    public ulong Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}
