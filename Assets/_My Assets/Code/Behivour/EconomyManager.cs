using TMPro;
using UnityEngine;
using System.Globalization;
using System.Collections.Generic;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;  

    [SerializeField] private EconomyData economyData;

    [Header("<size=15>[USER INTERFACE]")]
    [SerializeField] private List<TMP_Text> coinsTxtList = new List<TMP_Text>();

    int coinsCollection = 0;


    private void Awake()
    {
        instance = this;
        InitilizeEconomy();
    }

    private void InitilizeEconomy()
    {
        coinsCollection = PlayerPrefs.GetInt(ConstantKeys.AVAILABLE_COINS, 0);
        economyData.availableCoins = coinsCollection;

        UpdateCoinsUI(economyData.availableCoins);
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
}
