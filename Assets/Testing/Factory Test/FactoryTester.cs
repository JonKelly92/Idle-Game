using UnityEngine;
using UnityEngine.UI;

public class FactoryTester : MonoBehaviour
{
    /*
     
    Buttons:
        
        - Print Line

        - Print Factory Values

        - Add currency (tier 1)

        - Purchase upgrade event

     
    What to test:

        - Make sure the values are correct at the start of the test

        - Listen for when a purchase event is fired 
            - _purchaseUpgradeEvent.OnEventRaised += PurchaseUpgrade;
            - and then listen for the values to change
               - _factoryValuesSO.LevelSO.OnValueChanged.AddListener(LevelChanged);
               - _factoryValuesSO.PayoutAmountSO.OnValueChanged.AddListener(PayoutAmountChanged);
               - _factoryValuesSO.UpgradeCostSO.OnValueChanged.AddListener(UpgradeCostChanged);

        - Listen for when CurrencyTier1's value changes           
            - _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.AddListener(CurrencyTier1Changed);
            - and then listen for the value to change
                - _factoryValuesSO.IsUpgradeAffordableSO.OnValueChanged.AddListener(IsUpgradeAffordable);      
        
        - Time remaining until the next payout

     */

    [SerializeField] private Button _printLine;
    [SerializeField] private Button _printFactoryValues;
    [SerializeField] private Button _printCurrencyValues;
    [SerializeField] private Button _addCurrency;
    [SerializeField] private Button _purchaseUpgrade;


    [SerializeField] private GenericEventScriptableObject _purchaseUpgradeEvent;
    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;
    [SerializeField] private PlayerCurrencyManagerScriptableObject _playerCurrenyManagerSO;

    private void Awake()
    {
        _printLine.onClick.AddListener(PrintOutLine);
        _printFactoryValues.onClick.AddListener(PrintOutFactorySOValues);
        _printCurrencyValues.onClick.AddListener(PrintOutCurrencyValues);
        _addCurrency.onClick.AddListener(AddCurrencyTier1);
        _purchaseUpgrade.onClick.AddListener(PurchaseUpgrade);

        _purchaseUpgradeEvent.OnEventRaised += PurchaseUpgradeEventFired;

        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.AddListener(CurrencyTier1Changed);

        _factoryValuesSO.LevelSO.OnValueChanged.AddListener(LevelChanged);
        _factoryValuesSO.PayoutAmountSO.OnValueChanged.AddListener(PayoutAmountChanged);
        _factoryValuesSO.UpgradeCostSO.OnValueChanged.AddListener(UpgradeCostChanged);
        _factoryValuesSO.IsUpgradeAffordableSO.OnValueChanged.AddListener(IsUpgradeAffordable);
    }

    private void OnDestroy()
    {
        _printLine.onClick.RemoveAllListeners();
        _printFactoryValues.onClick.RemoveAllListeners();
        _printCurrencyValues.onClick.RemoveAllListeners();
        _addCurrency.onClick.RemoveAllListeners();
        _purchaseUpgrade.onClick.RemoveAllListeners();

        _purchaseUpgradeEvent.OnEventRaised -= PurchaseUpgradeEventFired;

        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.RemoveListener(CurrencyTier1Changed);

        _factoryValuesSO.LevelSO.OnValueChanged.RemoveListener(LevelChanged);
        _factoryValuesSO.PayoutAmountSO.OnValueChanged.RemoveListener(PayoutAmountChanged);
        _factoryValuesSO.UpgradeCostSO.OnValueChanged.RemoveListener(UpgradeCostChanged);
        _factoryValuesSO.IsUpgradeAffordableSO.OnValueChanged.RemoveListener(IsUpgradeAffordable);
    }

    private void PrintOutLine()
    {
        Debug.Log("==========================");
    }

    private void PrintOutFactorySOValues()
    {
        Debug.Log("Factory SO Values: ");

        Debug.Log("Level : " + _factoryValuesSO.LevelSO.Value.ToString());

        Debug.Log("PayoutAmount : " + _factoryValuesSO.PayoutAmountSO.Value.ToString());

        Debug.Log("UpgradeCost : " + _factoryValuesSO.UpgradeCostSO.Value.ToString());

        Debug.Log("IsUpgradeAffordable : " + _factoryValuesSO.IsUpgradeAffordableSO.Value.ToString());

        Debug.Log("PayoutTimeRemaining : " + _factoryValuesSO.PayoutTimeRemainingSO.Value.ToString());
    }

    private void PrintOutCurrencyValues()
    {
        Debug.Log("Currency Values: ");

        Debug.Log("Tier 1 : " + _playerCurrenyManagerSO.CurrencyTier1.Value.ToString());
    }

    private void AddCurrencyTier1()
    {
        _playerCurrenyManagerSO.AddTier1Currency(100);
    }

    private void PurchaseUpgrade()
    {
        _purchaseUpgradeEvent.SendEvent();
    }

    private void PurchaseUpgradeEventFired()
    {
        Debug.Log("PurchaseUpgradeEvent Fired");
    }

    private void CurrencyTier1Changed(ulong tier1Amount)
    {
        Debug.Log("CurrencyTier1Changed: " + tier1Amount.ToString());
    }

    private void LevelChanged(int level)
    {
        Debug.Log("LevelChanged : " + level.ToString());
    }

    private void PayoutAmountChanged(ulong payoutAmount)
    {
        Debug.Log("PayoutAmountChanged : " + payoutAmount.ToString());
    }

    private void UpgradeCostChanged(ulong upgradeCost)
    {
        Debug.Log("UpgradeCostChanged : " + upgradeCost.ToString());
    }

    private void IsUpgradeAffordable(bool isItAffordable)
    {
        Debug.Log("IsUpgradeAffordable : " + isItAffordable.ToString());
    }

    private void PayoutTimeRemainingChanged(float timeRemaining)
    {
        Debug.Log("PayoutTimeRemainingChanged : " + timeRemaining.ToString());
    }
}
