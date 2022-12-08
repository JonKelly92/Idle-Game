using UnityEngine;

public class UIManager_Factory : MonoBehaviour
{
    /*
      
        - on startup get the current values from _factoryValuesSO and update the UI
            - PayoutTimeRemaing, Level, Payout Amount, UpgradeCost
    
        - use _factoryValuesSO event to check for the PayoutTimeRemaing updates

        - use _factoryValuesSO event to update the Level

        - use _factoryValuesSO event to update the payout amount
    
        - use _factoryValuesSO event UpgradeCost update 
            - compare against _playerCurrenyManagerSO to see if it's afforable and lock/unlock upgrade button accordingly

        - when upgrade button is unlocked and pressed then trigger event in _factoryValuesSO
            - this will notify the factory that an upgrade should be purchased

     */

    [SerializeField] private FactoryValuesScriptableObject _factoryValuesSO;
    [SerializeField] private PlayerCurrencyManagerScriptableObject _playerCurrenyManagerSO;


    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
