using System;
using UnityEngine;

public static class FormatCurrency
{
    public static string ToCurrency(this double amount)
    {
        amount = Math.Truncate(amount);
        int length = amount.ToString().Length;

        if (length > 12)
            return amount.ToString("0,,,,.##T");
        else if (length > 9)
            return amount.ToString("0,,,.##B");
        else if (length > 6) 
            return amount.ToString("0,,.##M");
        else if (length > 3)
            return amount.ToString("0,.##K");
        else
            return amount.ToString("0");
    }
}
