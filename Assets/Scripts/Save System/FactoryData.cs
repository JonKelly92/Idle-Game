using System;

[Serializable]
public class FactoryData
{
    public int Level;
    public double PayoutAmount;
    public double UpgradeCost;

    public void SetData(int level, double payoutAmount, double upgradeCost)
    {
        Level = level;
        PayoutAmount = payoutAmount;
        UpgradeCost = upgradeCost;
    }
}