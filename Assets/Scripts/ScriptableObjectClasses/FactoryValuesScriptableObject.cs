using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FactoryValues")]
public class FactoryValuesScriptableObject : DescriptionBaseSO
{
    [field:SerializeField] public string FactoryName { get; private set; }
    [field:SerializeField] public float TimeBetweenPayouts { get; private set; }
    [field:SerializeField] public float BasePayoutAmount { get; private set; } // amount of money paid out after each pay period, that starting amount
    [field:SerializeField] public float PayoutMultiplier { get; private set; }// a percentage to increase the payout by after each upgrade
    [field:SerializeField] public float BaseUpgradeCost { get; private set; }// cost for the first upgrade
    [field: SerializeField] public float BaseUpgradeMultiplier { get; private set; }// a percentage to increase the cost by after each upgrade


    // The below variables send an event when their value is changed
    [field: SerializeField] public IntVariable LevelSO { get; private set; }
    [field: SerializeField] public DoubleVariable PayoutAmountSO { get; private set; }
    [field: SerializeField] public DoubleVariable UpgradeCostSO { get; private set; }
    [field: SerializeField] public FloatVariable PayoutTimeRemainingSO { get; private set; }
    [field: SerializeField]  public BoolVariable IsUpgradeAffordableSO { get; private set; }

}
