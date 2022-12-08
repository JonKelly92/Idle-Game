using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/FactoryValues")]
public class FactoryValuesScriptableObject : DescriptionBaseSO
{
    public UnityEvent<int> OnLevelChanged { get; private set; } = new UnityEvent<int>();

    [SerializeField] private int _level;
    public int Level
    {
        get => _level;
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
        get => _payoutAmount;
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
        get => _upgradeCost;
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

    public float TimeBetweenPayouts => _timeBetweenPayouts;
    public float BasePayoutAmount => _basePayoutAmount;
    public float PayoutMultiplier => _payoutMultiplier;
    public float BaseUpgradeCost => _baseUpgradeCost;
    public float BaseUpgradeMultiplier => _baseUpgradeMultiplier;
}
