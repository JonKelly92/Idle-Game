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


    // The below variables send an event when their value is changed (the above variables don't need this because they shouldn't be changed at runtime)

    [SerializeField] private IntVariable _level;
    [SerializeField] private uLongVariable _payoutAmount;
    [SerializeField] private uLongVariable _upgradeCost;
    [SerializeField] private FloatVariable _payoutTimeRemaining;
    [SerializeField] private BoolVariable _isUpgradeAffordable;

    public IntVariable LevelSO => _level;
    public uLongVariable PayoutAmountSO => _payoutAmount;
    public uLongVariable UpgradeCostSO => _upgradeCost;
    public FloatVariable PayoutTimeRemainingSO => _payoutTimeRemaining;
    public BoolVariable IsUpgradeAffordableSO => _isUpgradeAffordable;

}
