using UnityEngine;

public class InGameCoinCollection : MonoBehaviour
{
    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private EconomyData economyData;
    [SerializeField] private UpgradesData upgradesData;

    [Header("<size=15>[SCRIPTS]")]
    [SerializeField] private InGameUiManager inGameUiManager;
    [SerializeField] private EconomyManager ecnonomyManager;

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

        ecnonomyManager = EconomyManager.instance;
    }

    private void OnEnable()
    {
        NearMissTrigger.NearMiss += CoinsCollected;
        GameStatus.GameOverAction += SaveCoinsCollected;
    }

    private void OnDisable()
    {
        NearMissTrigger.NearMiss -= CoinsCollected;
        GameStatus.GameOverAction -= SaveCoinsCollected;
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

    public int GetCollectedCoins()
    {
        return (int)coinsCollectedThisRound;
    }
}
