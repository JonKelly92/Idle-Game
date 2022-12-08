using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/FactoryValues")]
public class FactoryValuesScriptableObject : DescriptionBaseSO
{
    [SerializeField] private string _factoryName;
    [SerializeField] private float _timeBetweenPayouts;
    [SerializeField] private float _basePayoutAmount;// amount of money paid out after each pay period, that starting amount
    [SerializeField] private float _payoutMultiplier;// a percentage to increase the payout by after each upgrade
    [SerializeField] private float _baseUpgradeCost;// cost for the first upgrade
    [SerializeField] private float _baseUpgradeMultiplier;// a percentage to increase the cost by after each upgrade

    public string FactoryName => _factoryName;
    public float TimeBetweenPayouts => _timeBetweenPayouts;
    public float BasePayoutAmount => _basePayoutAmount;
    public float PayoutMultiplier => _payoutMultiplier;
    public float BaseUpgradeCost => _baseUpgradeCost;
    public float BaseUpgradeMultiplier => _baseUpgradeMultiplier;


    public UnityEvent<int> OnLevelChanged { get; private set; } = new UnityEvent<int>();

    [SerializeField] private int _level;
    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            OnLevelChanged?.Invoke(_level);
        }
    }

    public UnityEvent<ulong> OnPayoutAmountChanged { get; private set; } = new UnityEvent<ulong>();

    [SerializeField] private ulong _payoutAmount;
    public ulong PayoutAmount
    {
        get => _payoutAmount;
        set
        {
            _payoutAmount = value;
            OnPayoutAmountChanged?.Invoke(_payoutAmount);
        }
    }

    public UnityEvent<ulong> OnUpgradeCostChanged { get; private set; } = new UnityEvent<ulong>();

    [SerializeField] private ulong _upgradeCost;
    public ulong UpgradeCost
    {
        get => _upgradeCost;
        set
        {
            _upgradeCost = value;
            OnUpgradeCostChanged?.Invoke(_upgradeCost);
        }
    }

    public UnityEvent<float> OnPayoutTimeRemainingChanged { get; private set; } = new UnityEvent<float>();

    [SerializeField] private float _payoutTimeRemaining;
    public float PayoutTimeRemaining
    {
        get => _payoutTimeRemaining;
        set
        {
            _payoutTimeRemaining = value;
            OnPayoutTimeRemainingChanged?.Invoke(_payoutTimeRemaining);
        }
    }

    public UnityEvent<bool> OnIsUpgradeAffordableChanged { get; private set; } = new UnityEvent<bool>();

    private bool _isUpgradeAffordable;
    public bool IsUpgradeAffordable
    {
        get => _isUpgradeAffordable;
        set
        {
            _isUpgradeAffordable = value;
            OnIsUpgradeAffordableChanged?.Invoke(_isUpgradeAffordable);
        }
    }
}
