using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "ScriptableObjects/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    public UnityEvent<float> OnValueChanged { get; private set; } = new UnityEvent<float>();

    [SerializeField] private float _value;

    public float Value
    {
        get { return _value; }
        set 
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}
