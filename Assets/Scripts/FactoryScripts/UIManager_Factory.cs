using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager_Factory : MonoBehaviour
{
    [SerializeField] private GenericEventScriptableObject _purchaseUpgradeEvent;
    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;
    [SerializeField] private PurchaseMultiplierScriptableObject _purchaseMultiplierSO;

    [SerializeField] private Color _upgradeBtnUnlocked;
    [SerializeField] private Color _upgradeBtnLocked;
    [SerializeField] private Button _upgradeBtn;

    [SerializeField] private GameObject _lock; // show the lock when the player can't afford to purchase this factory 
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _payoutAmount;
    [SerializeField] private TextMeshProUGUI _currentLevel; // this also shows the next level milestone i.e. 10/25
    [SerializeField] private TextMeshProUGUI _upgradeCost; // cost to purchase next level
    [SerializeField] private TextMeshProUGUI _purchaseMultiplier; // i.e. 1, 10, 100, Max


    private void Awake()
    {
        _factoryValuesSO.PayoutTimeRemainingSO.OnValueChanged.AddListener(PayoutTimeRemainingChanged);
        _factoryValuesSO.LevelSO.OnValueChanged.AddListener(LevelChanged);
        _factoryValuesSO.PayoutAmountSO.OnValueChanged.AddListener(PayoutAmountChanged);
        _factoryValuesSO.UpgradeCostSO.OnValueChanged.AddListener(UpgradeCostChanged);
        _factoryValuesSO.IsUpgradeAffordableSO.OnValueChanged.AddListener(IsUpgradeAffordable);

        _purchaseMultiplierSO.OnValueChanged.AddListener(PurchaseMultiplierChanged);

        _upgradeBtn.onClick.AddListener(PurchaseUpgrade);
    }

    private void Start()
    {
        // Init UI so it reflects the actual values
        PayoutTimeRemainingChanged(_factoryValuesSO.PayoutTimeRemainingSO.Value);
        LevelChanged(_factoryValuesSO.LevelSO.Value);
        PayoutAmountChanged(_factoryValuesSO.PayoutAmountSO.Value);
        UpgradeCostChanged(_factoryValuesSO.UpgradeCostSO.Value);

        if (_factoryValuesSO.LevelSO.Value > 0)
            _lock.SetActive(false);

        PurchaseMultiplierChanged(_purchaseMultiplierSO.Value);
    }

    private void OnDestroy()
    {
        _factoryValuesSO.PayoutTimeRemainingSO.OnValueChanged.RemoveListener(PayoutTimeRemainingChanged);
        _factoryValuesSO.LevelSO.OnValueChanged.RemoveListener(LevelChanged);
        _factoryValuesSO.PayoutAmountSO.OnValueChanged.RemoveListener(PayoutAmountChanged);
        _factoryValuesSO.UpgradeCostSO.OnValueChanged.RemoveListener(UpgradeCostChanged);
        _factoryValuesSO.IsUpgradeAffordableSO.OnValueChanged.RemoveListener(IsUpgradeAffordable);

        _purchaseMultiplierSO.OnValueChanged.RemoveListener(PurchaseMultiplierChanged);

        _upgradeBtn.onClick.RemoveAllListeners();
    }

    private void PayoutTimeRemainingChanged(double timeRemaining)
    {
        double percentageOfProgress = Math.Clamp((_factoryValuesSO.PayoutTimeRemainingSO.Value / _factoryValuesSO.TimeBetweenPayouts), 0, 1);

        _progressBar.fillAmount = (float)percentageOfProgress;
    }

    private void LevelChanged(int level)
    {
        if (_factoryValuesSO.LevelSO.Value > 0)
            _lock.SetActive(false);
        else
            _lock.SetActive(true); // this is primarily here for testing purposes

        _currentLevel.SetText(level.ToString());
    }

    private void PayoutAmountChanged(double payoutAmount)
    {
        _payoutAmount.SetText("$" + payoutAmount.ToCurrency());
    }

    private void UpgradeCostChanged(double upgradeCost)
    {
        _upgradeCost.SetText("$" + upgradeCost.ToCurrency());
    }

    private void IsUpgradeAffordable(bool isItAffordable)
    {
        if (isItAffordable)
            _upgradeBtn.image.color = _upgradeBtnUnlocked;
        else
            _upgradeBtn.image.color = _upgradeBtnLocked;
    }
    private void PurchaseMultiplierChanged(PurchaseMultiplierEnum multiplier)
    {
        _purchaseMultiplier.SetText("Buy " + multiplier.ToString());
    }


    private void PurchaseUpgrade()
    {
        _purchaseUpgradeEvent.SendEvent();
    }
}
