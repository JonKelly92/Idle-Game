using System;
using UnityEngine;

public static class FormatCurrency
{
    public static string ToCurrency(this double amount)
    {
        int length = amount.ToString("N2").Length;

        if (length > 18)
            return amount.ToString("0,,,,.##T");
        else if (length > 14)
            return amount.ToString("0,,,.##B");
        else if (length > 10) 
            return amount.ToString("0,,.##M");
        else if (length > 7)
            return amount.ToString("0,.##K");
        else
            return amount.ToString("0.##");
    }
}

/*
      100.00
    K 1,000.00 
    M 1,000,000.00
    B 1,000,000,000.00
    T 1,000,000,000,000.00
*/