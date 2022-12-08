using UnityEngine;

public class UIManager_Factory : MonoBehaviour
{
    [SerializeField] private GenericEventScriptableObject _purchaseUpgradeEvent;
    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;


    private void Awake()
    {
        _factoryValuesSO.PayoutTimeRemainingSO.OnValueChanged.AddListener(PayoutTimeRemainingChanged);
        _factoryValuesSO.LevelSO.OnValueChanged.AddListener(LevelChanged);
        _factoryValuesSO.PayoutAmountSO.OnValueChanged.AddListener(PayoutAmountChanged);
        _factoryValuesSO.UpgradeCostSO.OnValueChanged.AddListener(UpgradeCostChanged);
        _factoryValuesSO.IsUpgradeAffordableSO.OnValueChanged.AddListener(IsUpgradeAffordable);
    }

    private void Start()
    {
        // Init UI so it reflects the actual values
        PayoutTimeRemainingChanged(_factoryValuesSO.PayoutTimeRemainingSO.Value);
        LevelChanged(_factoryValuesSO.LevelSO.Value);
        PayoutAmountChanged(_factoryValuesSO.PayoutAmountSO.Value);
        UpgradeCostChanged(_factoryValuesSO.UpgradeCostSO.Value);
    }

    private void OnDestroy()
    {
        _factoryValuesSO.PayoutTimeRemainingSO.OnValueChanged.RemoveListener(PayoutTimeRemainingChanged);
        _factoryValuesSO.LevelSO.OnValueChanged.RemoveListener(LevelChanged);
        _factoryValuesSO.PayoutAmountSO.OnValueChanged.RemoveListener(PayoutAmountChanged);
        _factoryValuesSO.UpgradeCostSO.OnValueChanged.RemoveListener(UpgradeCostChanged);
        _factoryValuesSO.IsUpgradeAffordableSO.OnValueChanged.RemoveListener(IsUpgradeAffordable);
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
