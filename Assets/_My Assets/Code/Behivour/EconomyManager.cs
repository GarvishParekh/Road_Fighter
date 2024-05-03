using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Collections.Generic;

public class EconomyManager : MonoBehaviour
{
    public static Action<int> ConfirmPurchase;
    public static EconomyManager instance;  

    [SerializeField] private EconomyData economyData;

    [Header("<size=15>[COMPONENTS]")]
    [SerializeField] private CanvasCell confirmBuycanvas;

    [Header("<size=15>[USER INTERFACE]")]
    [SerializeField] private List<TMP_Text> coinsTxtList = new List<TMP_Text>();
    [SerializeField] private Button confirmBuyButton;
    [SerializeField] private TMP_Text areYouSureTxt;

    int coinsCollection = 0;


    private void Awake()
    {
        instance = this;
        InitilizeEconomy();
    }

    private void OnEnable()
    {
        ConfirmPurchase += SpendCoins;
    }

    private void OnDisable()
    {
        ConfirmPurchase -= SpendCoins;
    }

    private void InitilizeEconomy()
    {
        coinsCollection = PlayerPrefs.GetInt(ConstantKeys.AVAILABLE_COINS, economyData.defaultCoins);
        economyData.availableCoins = coinsCollection;

        UpdateCoinsUI(economyData.availableCoins);
        PlayerPrefs.SetInt(ConstantKeys.AVAILABLE_COINS, economyData.availableCoins);
    }

    private void UpdateCoinsUI(int _coinsToUpdate)
    {
        foreach (TMP_Text coinTxt in coinsTxtList)
        {
            coinTxt.text = _coinsToUpdate.ToString("#,##0", CultureInfo.InvariantCulture);
        }
    }

    public void AddCoins (int _coinsToAdd)
    {
        coinsCollection = PlayerPrefs.GetInt(ConstantKeys.AVAILABLE_COINS, 0);
        coinsCollection += _coinsToAdd;
        economyData.availableCoins = coinsCollection;
        PlayerPrefs.SetInt(ConstantKeys.AVAILABLE_COINS, economyData.availableCoins);

        UpdateCoinsUI(economyData.availableCoins);
    }

    private void SpendCoins(int _coinsToAdd)
    {
        coinsCollection = PlayerPrefs.GetInt(ConstantKeys.AVAILABLE_COINS, 0);
        coinsCollection -= _coinsToAdd;
        economyData.availableCoins = coinsCollection;
        PlayerPrefs.SetInt(ConstantKeys.AVAILABLE_COINS, economyData.availableCoins);

        UpdateCoinsUI(economyData.availableCoins);
    }

    public bool IsMoneyAvilable(int moneyRequried)
    {
        if (moneyRequried <= economyData.availableCoins)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Button GetConfirmBuyButton(int itemPrice, string itemName, PurchaseType purchaseType)
    {
        confirmBuyButton.onClick.RemoveAllListeners();
        confirmBuyButton.onClick.AddListener(confirmBuycanvas.CloseCanvas);
        switch (purchaseType)
        {
            case PurchaseType.BUY:
                areYouSureTxt.text = $"You you sure you want to \n buy <b><color=#DB3E34>{itemName}</b></color> for <color=#F1C40F><b>{itemPrice.ToString("#,##0", CultureInfo.InvariantCulture)} Coins";
                break;
            case PurchaseType.UPGRADE:
                areYouSureTxt.text = $"You you sure you want to \n upgrade <b><color=#DB3E34>{itemName}</b></color> for <color=#F1C40F><b>{itemPrice.ToString("#,##0", CultureInfo.InvariantCulture)} Coins";
                break;
        }
        return confirmBuyButton;
    }
}
