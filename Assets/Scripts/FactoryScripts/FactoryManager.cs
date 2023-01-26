using System;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    [SerializeField] private GenericEventScriptableObject _purchaseUpgradeEvent;
    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;
    [SerializeField] private TimerTickEvent _timerTickEvent;
    [SerializeField] private PlayerCurrencyManagerScriptableObject _playerCurrenyManagerSO;

    private void Awake()
    {
        _timerTickEvent.OnTimerTick.AddListener(OnTimerTick);

        _purchaseUpgradeEvent.OnEventRaised += PurchaseUpgrade;

        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.AddListener(CurrencyTier1Changed);
    }

    private void Start()
    {
        // Initialize values in case they have never been used before
        if (_factoryValuesSO.LevelSO.Value == 0)
            _factoryValuesSO.LevelSO.Value = 1;

        if (_factoryValuesSO.PayoutAmountSO.Value < _factoryValuesSO.BasePayoutAmount)
            _factoryValuesSO.PayoutAmountSO.Value = _factoryValuesSO.BasePayoutAmount;

        if (_factoryValuesSO.UpgradeCostSO.Value < _factoryValuesSO.BaseUpgradeCost)
            _factoryValuesSO.UpgradeCostSO.Value = _factoryValuesSO.BaseUpgradeCost;

        CurrencyTier1Changed(_playerCurrenyManagerSO.CurrencyTier1.Value);
    }

    private void OnDestroy()
    {
        _timerTickEvent.OnTimerTick.RemoveListener(OnTimerTick);

        _purchaseUpgradeEvent.OnEventRaised -= PurchaseUpgrade;

        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.RemoveListener(CurrencyTier1Changed);
    }

    // time recieved is in milliseconds
    private void OnTimerTick(double timeSinceLastTick)
    {
        if (timeSinceLastTick > _factoryValuesSO.PayoutTimeRemainingSO.Value)
        {
            int numberOfPayouts = 0;

            // subtract the remaining time from this pay out period and add 1 payout
            timeSinceLastTick -= _factoryValuesSO.PayoutTimeRemainingSO.Value; 
            numberOfPayouts++;

            // determine how many more pay out periods have passed and add that many payouts
            numberOfPayouts += (int)Math.Truncate(timeSinceLastTick / _factoryValuesSO.TimeBetweenPayouts); 

            // pay the player for each pay out
            _playerCurrenyManagerSO.AddTier1Currency(numberOfPayouts * _factoryValuesSO.PayoutAmountSO.Value);

            // this gives us the amount of time that has already passed since the start of this pay out period
            double timePassThisPeriod = timeSinceLastTick % _factoryValuesSO.TimeBetweenPayouts;
            _factoryValuesSO.PayoutTimeRemainingSO.Value = _factoryValuesSO.TimeBetweenPayouts - timePassThisPeriod;

        }
        else
        {
            _factoryValuesSO.PayoutTimeRemainingSO.Value -= timeSinceLastTick;
        }
    }

    private void PurchaseUpgrade()
    {
        if (_playerCurrenyManagerSO.SpendTier1Currency(_factoryValuesSO.UpgradeCostSO.Value))
        {
            // Upgrade was successfully purchased
            IncreaseLevel();
        }
        else
            Debug.LogWarning("Failed to purchase upgrade for " + _factoryValuesSO.FactoryName);
    }

    // The Tier 1 currency has changed (increased or decreased) so we check if the player can afford the next upgrade and then update the SO
    // Which in turn sends an event to update the UI
    private void CurrencyTier1Changed(double tier1Amount)
    {
        bool isUpgradeAffordableCheck = _playerCurrenyManagerSO.IsThisAfforable_Tier1(_factoryValuesSO.UpgradeCostSO.Value);
        _factoryValuesSO.IsUpgradeAffordableSO.Value = isUpgradeAffordableCheck;
    }

    private void IncreaseLevel() => SetLevel(_factoryValuesSO.LevelSO.Value + 1);

    private void SetLevel(int newLevel)
    {
        _factoryValuesSO.LevelSO.Value = newLevel;

        _factoryValuesSO.PayoutAmountSO.Value = _factoryValuesSO.PayoutMultiplier * _factoryValuesSO.PayoutAmountSO.Value;

        _factoryValuesSO.UpgradeCostSO.Value = _factoryValuesSO.BaseUpgradeMultiplier * _factoryValuesSO.UpgradeCostSO.Value;
    }
}