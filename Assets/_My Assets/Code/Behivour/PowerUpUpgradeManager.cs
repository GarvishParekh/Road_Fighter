using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PowerUpUpgradeManager : MonoBehaviour
{
    UiManager uiManager;
    EconomyManager economyManager;

    Upgrades selectedUpgrade;

    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private UpgradesData upgradesData;
    [SerializeField] private EconomyData economyData;

    [Header("<size=15>[SCRIPTS]")]
    [SerializeField] private MainMenuUIManager mainMenuUIManager;

    [Header("<size=15>[COMPONENTS]")]
    [SerializeField] private List<UpgradeCell> upgradeCellList = new List<UpgradeCell>();


    private void Start()
    {
        uiManager = UiManager.instance;
        economyManager = EconomyManager.instance;
    }

    public void UpgradeScoreMultiplier()
        => UpgadePowerUp(PowerupType.SCORE_MULTIPLIER);

    public void UpgradeMagnet()
        => UpgadePowerUp(PowerupType.MAGNET);

    public void UpgradeCoinMultiplier()
        => UpgadePowerUp(PowerupType.COIN_MULTIPLIER);

    private void UpgadePowerUp(PowerupType powerUpToUpgrade)
    {
        selectedUpgrade = upgradesData.upgrades[(int)powerUpToUpgrade];

        if (economyManager.IsMoneyAvilable(selectedUpgrade.requriedCoins[selectedUpgrade.upgradeLevel]))
        {
            // coins available
            Button buyButton = economyManager.GetConfirmBuyButton(selectedUpgrade.requriedCoins[selectedUpgrade.upgradeLevel], selectedUpgrade.upgradeName, PurchaseType.UPGRADE);
            buyButton.onClick.AddListener(SendToYesButton);

            uiManager.OpenPopupCanvas(CanvasCellsName.CONFIRM_BUY_POPUP);
        }
        else
        {
            // coins not available
            mainMenuUIManager.NoEnoughCanvas(economyData.availableCoins - selectedUpgrade.requriedCoins[selectedUpgrade.upgradeLevel]);
        }
    }

    public void SendToYesButton()
    {
        PowerupType powerUpToUpgrade = selectedUpgrade.powerupType;
        EconomyManager.ConfirmPurchase?.Invoke(selectedUpgrade.requriedCoins[selectedUpgrade.upgradeLevel]);
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
