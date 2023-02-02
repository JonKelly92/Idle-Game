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
    // it will then return the amount of levels the player can afford, either the amount of the muliplier or 0
    private int CalculateUpgradeCostForLevel(PurchaseMultiplierEnum multiplier)
    {
        int amountOfLevels = 0;

        string multiplierAsString = multiplier.ToString();
        multiplierAsString = multiplierAsString.Remove(0, 1);
        if (!int.TryParse(multiplierAsString, out amountOfLevels))
        {
            Debug.LogError("Failed to parse the multiplier enum into an integer");
            return 0;
        }

        int levels = _factoryValuesSO.LevelSO.Value + amountOfLevels;

        double costForUpgrade = _factoryValuesSO.BaseUpgradeCost * Math.Pow(_factoryValuesSO.BaseUpgradeMultiplier, levels);

        _factoryValuesSO.UpgradeCostSO.Value = costForUpgrade;

        return levels;
    }

    // Calulates the new Upgrade Cost value for the highest level the player can afford then it automatically updates it
    // if the player can't to upgrade at all then the Upgrade Cost value will still show the cost for x1 levels 
    // it will then return the amount of levels the player can afford 
    private int CalculateUpgradeCostForMaxLevels()
    {
        // TODO : 
        /*
            This function needs to do 2 things:
                - calculate how many factories the player can afford and return that value
                - calculate the cost of the factories and update _factoryValuesSO.UpgradeCostSO.Value
        */
    
        var amountOfLevels = 0;

        var n = 10; // number of factories to buy
        var b = _factoryValuesSO.BaseUpgradeCost;
        var r = _factoryValuesSO.BaseUpgradeMultiplier;
        var k = _factoryValuesSO.LevelSO.Value;
        var c = _playerCurrenyManagerSO.CurrencyTier1.Value;

        // The folowing 3 steps calculates the cost of N factories
        var step1 = Math.Pow(r, k) * (Math.Pow(r, n) - 1);
        var step2 = step1 / (r-1);
        var step3 = b * step2;

        // step 3 and test should give the same results

        var test = b * ((Math.Pow(r, k) * (Math.Pow(r, n) - 1)) / (r-1));


        //_factoryValuesSO.UpgradeCostSO.Value = upgradeCost;

        return (int)amountOfLevels;
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
            CheckIfUpgradeIsAffordable(); // we just re-calculated the upgrade cost so we should check if it's affordable to make sure the UI is displaying the correct information
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
