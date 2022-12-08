using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/IntVariable")]
public class IntVariable : DescriptionBaseSO
{
    public UnityEvent<int> OnValueChanged { get; private set; } = new UnityEvent<int>();

    [SerializeField] private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}
