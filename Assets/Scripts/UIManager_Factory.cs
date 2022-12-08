using UnityEngine;

public class UIManager_Factory : MonoBehaviour
{
    [SerializeField] private GenericEventScriptableObject _purchaseUpgradeEvent;
    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;


    private void Awake()
    {
        _factoryValuesSO.OnPayoutTimeRemainingChanged.AddListener(PayoutTimeRemainingChanged);
        _factoryValuesSO.OnLevelChanged.AddListener(LevelChanged);
        _factoryValuesSO.OnPayoutAmountChanged.AddListener(PayoutAmountChanged);
        _factoryValuesSO.OnUpgradeCostChanged.AddListener(UpgradeCostChanged);
        _factoryValuesSO.OnIsUpgradeAffordableChanged.AddListener(IsUpgradeAffordable);
    }

    private void Start()
    {
        // Init UI so it reflects the actual values
        PayoutTimeRemainingChanged(_factoryValuesSO.PayoutTimeRemaining);
        LevelChanged(_factoryValuesSO.Level);
        PayoutAmountChanged(_factoryValuesSO.PayoutAmount);
        UpgradeCostChanged(_factoryValuesSO.UpgradeCost);
    }

    private void OnDestroy()
    {
        _factoryValuesSO.OnPayoutTimeRemainingChanged.RemoveListener(PayoutTimeRemainingChanged);
        _factoryValuesSO.OnLevelChanged.RemoveListener(LevelChanged);
        _factoryValuesSO.OnPayoutAmountChanged.RemoveListener(PayoutAmountChanged);
        _factoryValuesSO.OnUpgradeCostChanged.RemoveListener(UpgradeCostChanged);
        _factoryValuesSO.OnIsUpgradeAffordableChanged.RemoveListener(IsUpgradeAffordable);
    }

    private void PayoutTimeRemainingChanged(float timeRemaining)
    {
        // TODO : update UI
    }

    private void LevelChanged(int level)
    {
        // TODO : update UI
    }

    private void PayoutAmountChanged(ulong payoutAmount)
    {
        // TODO : update UI
    }

    private void UpgradeCostChanged(ulong upgradeCost)
    {
        // TODO : update UI  
    }

    private void IsUpgradeAffordable(bool isItAffordable)
    {
        // TOOD : update UI
    }


    private void PurchaseUpgrade()
    {
        _purchaseUpgradeEvent.SendEvent();
    }
}
