using UnityEngine;

public enum PurchaseType
{
    BUY,
    UPGRADE
}

[CreateAssetMenu (fileName = "Economy Data", menuName = "Economy Data")]
public class EconomyData : ScriptableObject
{
    public int availableCoins = 0;
    public int defaultCoins = 10000;
}
