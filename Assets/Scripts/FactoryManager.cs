using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    /*
      
    
    
    - every timer tick determine how much time is left until the next pay out and update _factoryValuesSO
         - going to need a SO with the amount of time since the last tick
         - deduct that from the time remaing until pay out
         - if the time that has passed is more than the remaining time until the payout then it spills over into the next payout timer


     */

    [SerializeField] private GenericEventScriptableObject _purchaseUpgradeEvent;
    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;
    [SerializeField] private PlayerCurrencyManagerScriptableObject _playerCurrenyManagerSO;

    private void Awake()
    {
        _purchaseUpgradeEvent.OnEventRaised += PurchaseUpgrade;

        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.AddListener(CurrencyTier1Changed);
    }

    private void Start()
    {
        // Initialize values in case they have never been used before
        if (_factoryValuesSO.LevelSO.Value == 0)
            _factoryValuesSO.LevelSO.Value = 1;

        if (_factoryValuesSO.PayoutAmountSO.Value < _factoryValuesSO.BasePayoutAmount)
            _factoryValuesSO.PayoutAmountSO.Value = (ulong)_factoryValuesSO.BasePayoutAmount;

        if (_factoryValuesSO.UpgradeCostSO.Value < _factoryValuesSO.BaseUpgradeCost)
            _factoryValuesSO.UpgradeCostSO.Value = (ulong)_factoryValuesSO.BaseUpgradeCost;

        CurrencyTier1Changed(_playerCurrenyManagerSO.CurrencyTier1.Value);
    }

    private void OnDestroy()
    {
        _purchaseUpgradeEvent.OnEventRaised -= PurchaseUpgrade;

        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.RemoveListener(CurrencyTier1Changed);
    }

    private void PurchaseUpgrade()
    {
        if(_playerCurrenyManagerSO.SpendTier1Currency(_factoryValuesSO.UpgradeCostSO.Value))
        {
            // Upgrade was successfully purchased
            IncreaseLevel();
        }
        else
            Debug.LogError("Failed to purchase upgrade for " + _factoryValuesSO.FactoryName);
    }

    // The Tier 1 currency has changed (increased or decreased) so we check if the player can afford the next upgrade and then update the SO
    // Which in turn sends an event to update the UI
    private void CurrencyTier1Changed(ulong tier1Amount)
    {
        bool isUpgradeAffordableCheck = _playerCurrenyManagerSO.IsThisAfforable_Tier1(_factoryValuesSO.UpgradeCostSO.Value);
        _factoryValuesSO.IsUpgradeAffordableSO.Value = isUpgradeAffordableCheck;
    }

    private void IncreaseLevel() => SetLevel(_factoryValuesSO.LevelSO.Value++);

    private void SetLevel(int newLevel)
    {
        _factoryValuesSO.LevelSO.Value = newLevel;

        _factoryValuesSO.PayoutAmountSO.Value = (ulong)_factoryValuesSO.PayoutMultiplier * _factoryValuesSO.PayoutAmountSO.Value;

        _factoryValuesSO.UpgradeCostSO.Value = (ulong)_factoryValuesSO.BaseUpgradeMultiplier * _factoryValuesSO.UpgradeCostSO.Value;
    }
}
