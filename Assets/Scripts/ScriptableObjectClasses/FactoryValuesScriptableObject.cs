using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FactoryValues", menuName = "ScriptableObjects/FactoryValues")]
public class FactoryValuesScriptableObject : ScriptableObject
{
    public UnityEvent<int> OnLevelChanged { get; private set; } = new UnityEvent<int>();

    [SerializeField] private int _level;
    public int Level
    {
        get { return _level; }
        private set
        {
            _level = value;
            OnLevelChanged?.Invoke(_level);
        }
    }

    public UnityEvent<float> OnPayoutAmountChanged { get; private set; } = new UnityEvent<float>();

    [SerializeField] private float _payoutAmount;
    public float PayoutAmount
    {
        get { return _payoutAmount; }
        private set
        {
            _payoutAmount = value;
            OnPayoutAmountChanged?.Invoke(_payoutAmount);
        }
    }

    public UnityEvent<float> OnUpgradeCostChanged { get; private set; } = new UnityEvent<float>();

    [SerializeField] private float _upgradeCost;
    public float UpgradeCost
    {
        get { return _upgradeCost; }
        private set
        {
            _upgradeCost = value;
            OnUpgradeCostChanged?.Invoke(_upgradeCost);
        }
    }

    [SerializeField] private float _timeBetweenPayouts;
    [SerializeField] private float _basePayoutAmount;// amount of money paid out after each pay period, that starting amount
    [SerializeField] private float _payoutMultiplier;// a percentage to increase the payout by after each upgrade
    [SerializeField] private float _baseUpgradeCost;// cost for the first upgrade
    [SerializeField] private float _baseUpgradeMultiplier;// a percentage to increase the cost by after each upgrade
    public float TimeBetweenPayouts { get => _timeBetweenPayouts; }
    public float BasePayoutAmount { get => _basePayoutAmount; }
    public float PayoutMultiplier { get => _payoutMultiplier; }
    public float BaseUpgradeCost { get => _baseUpgradeCost; }
    public float BaseUpgradeMultiplier { get => _baseUpgradeMultiplier; }


    /// <summary>
    /// Increases UpgradeLevel by 1
    /// </summary>
    public void IncreaseLevel() => SetLevel(Level++);

    /// <summary>
    /// Sets UpgradeLevel to specified number
    /// </summary>
    /// <param name="newLevel"></param>
    public void SetLevel(int newLevel)
    {
        Level = newLevel;

        PayoutAmount = PayoutMultiplier * PayoutAmount;

        UpgradeCost = BaseUpgradeMultiplier * UpgradeCost;
    }

}
