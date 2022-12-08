using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/FloatVariable")]
public class FloatVariable : DescriptionBaseSO
{
    public UnityEvent<float> OnValueChanged { get; private set; } = new UnityEvent<float>();

    [SerializeField] private float _value;

    public float Value
    {
        get => _value;
        set 
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}
