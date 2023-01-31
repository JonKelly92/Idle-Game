using UnityEngine;
using UnityEngine.Events;

// entries in this enum must only be Max or a number starting with x. Max must always be at the end of the list.
public enum PurchaseMultiplierEnum
{
    x1 = 0,
    x10 = 1,
    x100 = 2,
    Max = 3
}

[CreateAssetMenu(fileName = "PurchaseMultiplier", menuName = "ScriptableObjects/PurchaseMultiplier")]
public class PurchaseMultiplierScriptableObject : DescriptionBaseSO
{
    public UnityEvent<PurchaseMultiplierEnum> OnValueChanged { get; private set; } = new UnityEvent<PurchaseMultiplierEnum>();

    [SerializeField] private PurchaseMultiplierEnum _value;

    public PurchaseMultiplierEnum Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }
}
