using UnityEngine;
using System.Collections.Generic;

public enum PowerupType
{
    SCORE_MULTIPLIER,
    MAGNET,
    COIN_MULTIPLIER

}

[CreateAssetMenu(fileName = "Upgrades Data", menuName = "Upgrades Data")]
public class UpgradesData : ScriptableObject
{
    public List<Upgrades> upgrades = new List<Upgrades>();
    public Color upgradeActiveColor;
    public Color upgradeDeactiveColor;

    public Sprite upgradeActiveSprite;
    public Sprite upgradeDeactiveSprite;

    public int maxUpgradeLevel = 5;
}

[System.Serializable]
public class Upgrades
{
    public string upgradeName = string.Empty;
    public PowerupType powerupType;
    public Sprite upgradeIcon;
    public int upgradeLevel = 0;

    public float[] levelValues;
    public int[] requriedCoins;
}
