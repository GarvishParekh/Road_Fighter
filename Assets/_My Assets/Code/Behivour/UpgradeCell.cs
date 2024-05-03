using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Globalization;

public class UpgradeCell : MonoBehaviour
{
    [SerializeField] private UpgradesData upgradesData;

    [SerializeField] private int myIndex;
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private TMP_Text upgradeNameTxt;
    [SerializeField] private TMP_Text requriedCoinsTxt;
    [SerializeField] private List<Image> upgradeBar = new List<Image>();
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private GameObject upgradeCompleted;

    Upgrades myUpgrade;

    private void Start()
    {
        myIndex = transform.GetSiblingIndex();
        myUpgrade = upgradesData.upgrades[myIndex];
        InitilizeUpgradeCell();
    }

    public void InitilizeUpgradeCell()
    {
        switch (myUpgrade.powerupType)
        {
            case PowerupType.SCORE_MULTIPLIER:
                myUpgrade.upgradeLevel = PlayerPrefs.GetInt(ConstantKeys.SCORE_MULTIPLIER, 0);
                break;
            case PowerupType.MAGNET:
                myUpgrade.upgradeLevel = PlayerPrefs.GetInt(ConstantKeys.MAGNET, 0);
                break;
            case PowerupType.COIN_MULTIPLIER:
                myUpgrade.upgradeLevel = PlayerPrefs.GetInt(ConstantKeys.COIN_MULTIPLIER, 0);
                break;
        }

        upgradeIcon.sprite = myUpgrade.upgradeIcon;
        upgradeNameTxt.text = myUpgrade.upgradeName;
        requriedCoinsTxt.text = myUpgrade.requriedCoins[myUpgrade.upgradeLevel].ToString("#,##0", CultureInfo.InvariantCulture);

        foreach (Image bar in upgradeBar)
        {
            int barIndex = bar.transform.GetSiblingIndex();
            if (barIndex <= myUpgrade.upgradeLevel)
            {
                //bar.sprite = upgradesData.upgradeActiveSprite;
                bar.color = upgradesData.upgradeActiveColor;
            }
            else
            {
                //bar.sprite = upgradesData.upgradeActiveSprite;
                bar.color = upgradesData.upgradeDeactiveColor;
            }

            TMP_Text levelUpgradeTxt = bar.GetComponentInChildren<TMP_Text>();
            switch (myUpgrade.powerupType)
            {
                case PowerupType.SCORE_MULTIPLIER:
                    levelUpgradeTxt.text = $"{myUpgrade.levelValues[bar.transform.GetSiblingIndex()] + 1}X";
                    break;
                case PowerupType.MAGNET:
                    myUpgrade.upgradeLevel = PlayerPrefs.GetInt(ConstantKeys.MAGNET, 0);
                    levelUpgradeTxt.text = myUpgrade.levelValues[bar.transform.GetSiblingIndex()].ToString()+"S";
                    break;
                case PowerupType.COIN_MULTIPLIER:
                    myUpgrade.upgradeLevel = PlayerPrefs.GetInt(ConstantKeys.COIN_MULTIPLIER, 0);
                    levelUpgradeTxt.text = myUpgrade.levelValues[bar.transform.GetSiblingIndex()].ToString()+"X";
                    break;
            }
        }

        if (myUpgrade.upgradeLevel < upgradesData.maxUpgradeLevel)
        {
            upgradeButton.SetActive(true);
            upgradeCompleted.SetActive(false);
        }
        else
        {
            upgradeButton.SetActive(false);
            upgradeCompleted.SetActive(true);
        }
    }
}
