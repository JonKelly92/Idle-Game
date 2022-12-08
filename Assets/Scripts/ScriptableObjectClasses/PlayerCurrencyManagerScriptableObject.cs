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
    /// Pass in how much you would like to spend, it will try to spend that amount and return true if there is sufficient funds (succeeded)
    /// </summary>
    /// <param name="amountToSpend"></param>
    /// <returns></returns>
    public bool SpendTier1Currency(ulong amountToSpend)
    {
        if(IsThisAfforable_Tier1(amountToSpend))
        {
            _currencyTier1.Value -= amountToSpend;

            return true;
        }
        else
            return false;
    }
}
