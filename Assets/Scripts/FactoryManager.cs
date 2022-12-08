using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;
    [SerializeField] private PlayerCurrencyManagerScriptableObject _playerCurrenyManagerSO;
    [SerializeField] private GenericEventScriptableObject _timerTickEvent;


    /*
        - every timer tick determine how much time is left until the next pay out and update _factoryValuesSO
        
        - Upgrade (recieve event from UI?, do it through _factoryValuesSO)
            - Check with _playerCurrenyManagerSO to see if we can afford to upgrade
            - if Yes then tell _playerCurrenyManagerSO to deduct the cost of the upgrade
            - Update factory's level, pay out amount and upgrade cost
        

    /// <summary>
    /// Increases Level by 1
    /// </summary>
    public void IncreaseLevel() => SetLevel(Level++);

    /// <summary>
    /// Sets Level to specified number
    /// </summary>
    /// <param name="newLevel"></param>
    public void SetLevel(int newLevel)
    {
        Level = newLevel;

        PayoutAmount = _payoutMultiplier * PayoutAmount;

        UpgradeCost = _baseUpgradeMultiplier * UpgradeCost;
    }

     */
}
