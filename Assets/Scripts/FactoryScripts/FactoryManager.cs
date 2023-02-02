using System;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    [SerializeField] private GenericEventScriptableObject _purchaseUpgradeEvent;
    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;
    [SerializeField] private PurchaseMultiplierScriptableObject _purchaseMultiplierSO;
    [SerializeField] private TimerTickEvent _timerTickEvent;
    [SerializeField] private PlayerCurrencyManagerScriptableObject _playerCurrenyManagerSO;

    private void Awake()
    {
        _timerTickEvent.OnTimerTick.AddListener(OnTimerTick);
        _purchaseMultiplierSO.OnValueChanged.AddListener(PurchaseMultiplierChanged);
        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.AddListener(CurrencyTier1Changed);

        _purchaseUpgradeEvent.OnEventRaised += PurchaseUpgrade;
    }

    private void Start()
    {
        // Initialize values in case they have never been used before
        if (_factoryValuesSO.PayoutAmountSO.Value < _factoryValuesSO.BasePayoutAmount)
            _factoryValuesSO.PayoutAmountSO.Value = _factoryValuesSO.BasePayoutAmount;

        if (_factoryValuesSO.UpgradeCostSO.Value < _factoryValuesSO.BaseUpgradeCost)
            _factoryValuesSO.UpgradeCostSO.Value = _factoryValuesSO.BaseUpgradeCost;

        // This makes sure the UI is displaying the correct values
        CurrencyTier1Changed(_playerCurrenyManagerSO.CurrencyTier1.Value);
        CalculateUpgradeCostForMultiplier(_purchaseMultiplierSO.Value);
        CheckIfUpgradeIsAffordable();
    }

    private void OnDestroy()
    {
        _timerTickEvent.OnTimerTick.RemoveListener(OnTimerTick);
        _purchaseMultiplierSO.OnValueChanged.RemoveListener(PurchaseMultiplierChanged);
        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.RemoveListener(CurrencyTier1Changed);

        _purchaseUpgradeEvent.OnEventRaised -= PurchaseUpgrade;
    }

    #region Events

    // time is recieved in milliseconds
    private void OnTimerTick(double timeSinceLastTick)
    {
        CalculatePayout(timeSinceLastTick);
    }

    private void PurchaseMultiplierChanged(PurchaseMultiplierEnum multiplier)
    {
        CalculateUpgradeCostForMultiplier(multiplier);
        CheckIfUpgradeIsAffordable();
    }

    private void PurchaseUpgrade()
    {
        PurchaseLevelUpgrade();
    }

    private void CurrencyTier1Changed(double tier1Amount)
    {
        if (_purchaseMultiplierSO.Value == PurchaseMultiplierEnum.Max)
            CalculateUpgradeCostForMaxLevels();

        CheckIfUpgradeIsAffordable();
    }

    #endregion

    // Calculating if there payout period has ended, how many payout periods have ended (offline earnings) and how much the player should be paid
    private void CalculatePayout(double timeSinceLastTick)
    {
        // the player needs to upgrade to level 1 to start getting payouts
        if (_factoryValuesSO.LevelSO.Value == 0)
            return;

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

    private bool CheckIfUpgradeIsAffordable()
    {
        bool isUpgradeAffordableCheck = _playerCurrenyManagerSO.IsThisAfforable_Tier1(_factoryValuesSO.UpgradeCostSO.Value);
        _factoryValuesSO.IsUpgradeAffordableSO.Value = isUpgradeAffordableCheck;

        return isUpgradeAffordableCheck;
    }

    private int CalculateUpgradeCostForMultiplier(PurchaseMultiplierEnum multiplier)
    {
        int levelsPlayerCanAfford = 0;

        if (multiplier != PurchaseMultiplierEnum.Max)
            levelsPlayerCanAfford = CalculateUpgradeCostForLevel(multiplier);
        else
            levelsPlayerCanAfford = CalculateUpgradeCostForMaxLevels();

        return levelsPlayerCanAfford;
    }

    // Calulates the new Upgrade Cost value and updates it
    // it will then return the amount of levels the player would have if they purchased the upgrades (i.e. selected x100 then returns 100 + current level)
    private int CalculateUpgradeCostForLevel(PurchaseMultiplierEnum multiplier)
    {
        int n = 0;

        string multiplierAsString = multiplier.ToString();
        multiplierAsString = multiplierAsString.Remove(0, 1);
        if (!int.TryParse(multiplierAsString, out n))
        {
            Debug.LogError("Failed to parse the multiplier enum into an integer");
            return 0;
        }

        var b = _factoryValuesSO.BaseUpgradeCost;
        var r = _factoryValuesSO.BaseUpgradeMultiplier;
        var k = _factoryValuesSO.LevelSO.Value;
        var c = _playerCurrenyManagerSO.CurrencyTier1.Value;

        // Calculates the cost of N factories
        var cost = b * ((Math.Pow(r, k) * (Math.Pow(r, n) - 1)) / (r-1)); 

        _factoryValuesSO.UpgradeCostSO.Value = cost;

        return _factoryValuesSO.LevelSO.Value + n;
    }

    // Calculates how many level upgrades the player can afford and the cost of the those upgrades
    // Returns the amount of level upgrades the player can afford and updates _factoryValuesSO.UpgradeCostSO.Value
    private int CalculateUpgradeCostForMaxLevels()
    {
        var b = _factoryValuesSO.BaseUpgradeCost;
        var r = _factoryValuesSO.BaseUpgradeMultiplier;
        var k = _factoryValuesSO.LevelSO.Value;
        var c = _playerCurrenyManagerSO.CurrencyTier1.Value;

        // this is a long equation so I broke it up into 2 parts to make it more readable
        var step1 = (c * (r - 1) / (b * Math.Pow(r, k)))+1;
        var n = Math.Floor(Math.Log(step1) / Math.Log(r)); // number of upgrades the player can afford

        // Calculates the cost of N factories
        var cost = b * ((Math.Pow(r, k) * (Math.Pow(r, n) - 1)) / (r-1));

        _factoryValuesSO.UpgradeCostSO.Value = cost;

        return (int)n;
    }

    // Presumably the player has upgraded their factory and now we are calculating how much they get paid
    private void CalculateNewPayoutAmount()
    {
        var payoutAmount = (_factoryValuesSO.BasePayoutAmount * _factoryValuesSO.LevelSO.Value) * _factoryValuesSO.PayoutMultiplier;

        _factoryValuesSO.PayoutAmountSO.Value = payoutAmount;
    }

    private void PurchaseLevelUpgrade()
    {
        int amountOfLevels = CalculateUpgradeCostForMultiplier(_purchaseMultiplierSO.Value);

        if(amountOfLevels == 0)
        {
            CheckIfUpgradeIsAffordable(); // we just re-calculated the upgrade cost so we should check if the upgrade is affordable to make sure the UI is displaying the correct information
            return; 
        }

        // attempt to purchase the upgrade
        if (!_playerCurrenyManagerSO.SpendTier1Currency(_factoryValuesSO.UpgradeCostSO.Value))
        {
            Debug.LogWarning("Failed to purchase upgrade for " + _factoryValuesSO.FactoryName);
            return;
        }

        // Set Level to amount of levels
        _factoryValuesSO.LevelSO.Value = amountOfLevels;

        // Calculate the new payout amount
        CalculateNewPayoutAmount();

        // Calculate the cost for the next upgrade
        CalculateUpgradeCostForMultiplier(_purchaseMultiplierSO.Value);

        // Check to see if the player can afford the next upgrade
        CheckIfUpgradeIsAffordable();
    }
}
