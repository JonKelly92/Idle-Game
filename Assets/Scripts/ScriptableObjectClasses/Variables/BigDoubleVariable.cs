using BreakInfinity;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/IntVariable")]
public class BigDoubleVariable : DescriptionBaseSO
{
    public UnityEvent<BigDouble> OnValueChanged { get; private set; } = new UnityEvent<BigDouble>();

    [SerializeField] private BigDouble _value;

    public BigDouble Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}
