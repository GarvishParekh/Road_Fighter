using UnityEngine;
using System.Collections.Generic;


public class PowerUpUpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradesData upgradesData;
    [SerializeField] private List<UpgradeCell> upgradeCellList = new List<UpgradeCell>();

    public void UpgradeScoreMultiplier()
        => UpgadePowerUp(PowerupType.SCORE_MULTIPLIER);

    public void UpgradeMagnet()
        => UpgadePowerUp(PowerupType.MAGNET);

    public void UpgradeCoinMultiplier()
        => UpgadePowerUp(PowerupType.COIN_MULTIPLIER);

    private void UpgadePowerUp(PowerupType powerUpToUpgrade)
    {
        Upgrades selectedUpgrade = upgradesData.upgrades[(int)powerUpToUpgrade];

        if (selectedUpgrade.upgradeLevel < 5)
        {
            selectedUpgrade.upgradeLevel += 1;
        }

        switch (powerUpToUpgrade)
        {
            case PowerupType.SCORE_MULTIPLIER:
                PlayerPrefs.SetInt(ConstantKeys.SCORE_MULTIPLIER, selectedUpgrade.upgradeLevel);
                break;
            case PowerupType.MAGNET:
                PlayerPrefs.SetInt(ConstantKeys.MAGNET, selectedUpgrade.upgradeLevel);
                break;
            case PowerupType.COIN_MULTIPLIER:
                PlayerPrefs.SetInt(ConstantKeys.COIN_MULTIPLIER, selectedUpgrade.upgradeLevel);
                break;
        }

        foreach (UpgradeCell cell in upgradeCellList)
        {
            cell.InitilizeUpgradeCell();
        }
    }
}
