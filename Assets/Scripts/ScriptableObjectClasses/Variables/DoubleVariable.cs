using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/DoubleVariable")]
public class DoubleVariable : DescriptionBaseSO
{
    public UnityEvent<double> OnValueChanged { get; private set; } = new UnityEvent<double>();

    [SerializeField] private double _value;

    public double Value
    {
        get => _value;
        set
        {
            _value = Math.Round(value, 2);
            OnValueChanged?.Invoke(_value);
        }
    }
}
