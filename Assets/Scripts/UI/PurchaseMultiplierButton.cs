using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseMultiplierButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _multiplierBtnText;
    [SerializeField] private Button _multiplierBtn;

    [SerializeField] private PurchaseMultiplierScriptableObject _purchaseMultiplierSO;

    private void Awake()
    {
        _purchaseMultiplierSO.OnValueChanged.AddListener(UpdateText);

        _multiplierBtn.onClick.AddListener(OnBtnClick);
    }

    private void Start()
    {
        UpdateText(_purchaseMultiplierSO.Value);
    }

    private void OnDestroy()
    {
        _purchaseMultiplierSO.OnValueChanged.RemoveListener(UpdateText);

        _multiplierBtn.onClick.RemoveAllListeners();
    }

    private void OnBtnClick()
    {
        PurchaseMultiplierEnum nextMultiplier;

        if (_purchaseMultiplierSO.Value == PurchaseMultiplierEnum.Max)
            nextMultiplier = (PurchaseMultiplierEnum)0;
        else
        {
            int currnetMultiplier = (int)_purchaseMultiplierSO.Value;
            currnetMultiplier++;
            nextMultiplier = (PurchaseMultiplierEnum)currnetMultiplier;
        }

        _purchaseMultiplierSO.Value = nextMultiplier;        
    }

    private void UpdateText(PurchaseMultiplierEnum multiplier)
    {
        _multiplierBtnText.SetText(_purchaseMultiplierSO.Value.ToString());
    }
}
