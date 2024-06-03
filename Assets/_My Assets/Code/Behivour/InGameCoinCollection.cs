using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InGameCoinCollection : MonoBehaviour
{
    UiManager uiManager;
    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private EconomyData economyData;
    [SerializeField] private UpgradesData upgradesData;

    [Header("<size=15>[SCRIPTS]")]
    [SerializeField] private InGameUiManager inGameUiManager;
    [SerializeField] private EconomyManager ecnonomyManager;

    [Header("<size=15>[UI]")]
    [SerializeField] private TMP_Text finalCoinTxt;

    [Header ("<size=15>[VALUES]")]
    [SerializeField] private float coinsCollectedThisRound;
    [SerializeField] private float coinsAddedOnCollection = 0;

    Upgrades coinUpgrades;


    private void Awake()
    {
        coinUpgrades = upgradesData.upgrades[2];
        coinsAddedOnCollection = economyData.coinsPerCollection * coinUpgrades.levelValues[coinUpgrades.upgradeLevel];
    }

    private void Start()
    {
        uiManager = UiManager.instance;
        ecnonomyManager = EconomyManager.instance;
    }

    private void OnEnable()
    {
        NearMissTrigger.NearMiss += CoinsCollected;
        GameStatus.GameOverAction += SaveCoinsCollected;
        AdsManager.GetReward += DoubleUpCoins;
    }

    private void OnDisable()
    {
        NearMissTrigger.NearMiss -= CoinsCollected;
        GameStatus.GameOverAction -= SaveCoinsCollected;
        AdsManager.GetReward -= DoubleUpCoins;
    }

    private void CoinsCollected(NearMissSide nearMissSideDummy)
    {
        coinsCollectedThisRound += coinsAddedOnCollection;
        coinsCollectedThisRound = Mathf.RoundToInt(coinsCollectedThisRound);    
        inGameUiManager.UpdateCoinsCountTxt((int)coinsCollectedThisRound);
    }

    public void SaveCoinsCollected()
    {
        ecnonomyManager.AddCoins((int)coinsCollectedThisRound);
        economyData.coinsCollectedPerRound = (int)coinsCollectedThisRound;

    }

    public void DoubleUpCoins ()
    {
        SaveCoinsCollected();
        finalCoinTxt.text = (coinsCollectedThisRound * 2).ToString("#,##0", CultureInfo.InvariantCulture);
        uiManager.OpenPopupCanvas(CanvasCellsName.DOUBLE_COINS_DONE);
    }

    public int GetCollectedCoins()
    {
        return (int)coinsCollectedThisRound;
    }
}
