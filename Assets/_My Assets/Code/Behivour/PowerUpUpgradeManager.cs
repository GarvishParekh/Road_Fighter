using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

        int upgradePrice = selectedUpgrade.requriedCoins[selectedUpgrade.upgradeLevel];
        string upgradeName = selectedUpgrade.upgradeName;

        if (economyManager.IsMoneyAvilable(upgradePrice))
        {
            // coins available
            Button buyButton = economyManager.GetConfirmBuyButton(upgradePrice, upgradeName, PurchaseType.UPGRADE);
            buyButton.onClick.AddListener(SendToYesButton);

            uiManager.OpenPopupCanvas(CanvasCellsName.CONFIRM_BUY_POPUP);
        }
        else
        {
            // coins not available
            int availabeCoins = economyData.availableCoins;
            mainMenuUIManager.NoEnoughCanvas(upgradePrice - availabeCoins);
        }
    }

    
    public void SendToYesButton()
    {
        PowerupType powerUpToUpgrade = selectedUpgrade.powerupType;
        EconomyManager.ConfirmPurchase?.Invoke(selectedUpgrade.requriedCoins[selectedUpgrade.upgradeLevel]);

        if (selectedUpgrade.upgradeLevel < upgradesData.maxUpgradeLevel)
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
