using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCurrencyManager", menuName = "ScriptableObjects/PlayerCurrencyManager")]
public class PlayerCurrencyManagerScriptableObject : ScriptableObject
{
    /*
        - SOs for each currency type
            - tier 1 currency - basic
            - tier 2 currency - difficult to aquire
            - tier 3 currency - premium 

        TODO: switch from using doubles to a float and use a letter to indicate size
        
     */

    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public DoubleVariable CurrencyTier1 { get; private set; }
    [field: SerializeField] public DoubleVariable CurrencyTier2 { get; private set; }


    /// <summary>
    /// Pass in the cost of what you would like to purchase and if the player can afford it then it returns true
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool IsThisAfforable_Tier1(double cost)
    {
        if (cost <= CurrencyTier1.Value)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Pass in how much you would like to spend, it will try to spend that amount and return true if there is sufficient funds (succeeded)
    /// </summary>
    /// <param name="amountToSpend"></param>
    /// <returns></returns>
    public bool SpendTier1Currency(double amountToSpend)
    {
        if (IsThisAfforable_Tier1(amountToSpend))
        {
            CurrencyTier1.Value -= amountToSpend;

            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Increases the amount of CurrencyTier1 that the play has
    /// </summary>
    /// <param name="amountGained"></param>
    public void AddTier1Currency(double amountGained)
    {
        CurrencyTier1.Value += amountGained;
    }
}
