using TMPro;
using UnityEngine;

public class UIManager_TopBar : MonoBehaviour
{
    [SerializeField] private PlayerCurrencyManagerScriptableObject _playerCurrenyManagerSO;

    [SerializeField] private TextMeshProUGUI _currencyTier1;
    [SerializeField] private TextMeshProUGUI _currencyTier2;


    private void Awake()
    {
        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.AddListener(CurrencyTier1Changed);
        _playerCurrenyManagerSO.CurrencyTier2.OnValueChanged.AddListener(CurrencyTier2Changed);
    }

    private void OnDestroy()
    {
        _playerCurrenyManagerSO.CurrencyTier1.OnValueChanged.RemoveListener(CurrencyTier1Changed);
        _playerCurrenyManagerSO.CurrencyTier2.OnValueChanged.RemoveListener(CurrencyTier2Changed);
    }

    private void CurrencyTier1Changed(double tier1Amount) => _currencyTier1.SetText(tier1Amount.ToString());

    private void CurrencyTier2Changed(double tier2Amount) => _currencyTier2.SetText(tier2Amount.ToString());

}
