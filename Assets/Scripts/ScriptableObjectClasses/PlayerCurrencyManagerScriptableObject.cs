using UnityEngine;

[CreateAssetMenu(fileName ="PlayerCurrencyManager", menuName = "ScriptableObjects/PlayerCurrencyManager")]
public class PlayerCurrencyManagerScriptableObject : ScriptableObject
{
    /*
        - SOs for each currency type
            - tier 1 currency - basic
            - tier 2 currency - difficult to aquire
            - tier 3 currency - premium 

        TODO: switch from using ulongs to a float and use a letter to indicate size
        
     */

    [SerializeField] private uLongVariable _currencyTier1;

    public uLongVariable CurrencyTier1 => _currencyTier1;


    /// <summary>
    /// Pass in the cost of what you would like to purchase and if the player can afford it then it returns true
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool IsThisAfforable_Tier1(ulong cost)
    {
        if(cost >= _currencyTier1.Value)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Pass in how much you would like to deduct, it will try to deduct it and return true if it succeeds 
    /// </summary>
    /// <param name="amountToDeduct"></param>
    /// <returns></returns>
    public bool DeductTier1Currency(ulong amountToDeduct)
    {
        if(IsThisAfforable_Tier1(amountToDeduct))
        {
            _currencyTier1.Value -= amountToDeduct;

            return true;
        }
        else
            return false;
    }
}
