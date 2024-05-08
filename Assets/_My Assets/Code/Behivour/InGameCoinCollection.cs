using UnityEngine;

public class InGameCoinCollection : MonoBehaviour
{
    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private EconomyData economyData;

    [Header("<size=15>[SCRIPTS]")]
    [SerializeField] private InGameUiManager inGameUiManager;

    [Header ("<size=15>[VALUES]")]
    [SerializeField] private int coinsCollectedThisRound;

    private void OnEnable()
    {
        NearMissTrigger.NearMiss += CoinsCollected;
    }

    private void OnDisable()
    {
        NearMissTrigger.NearMiss -= CoinsCollected;
    }

    private void CoinsCollected()
    {
        coinsCollectedThisRound += economyData.coinsPerCollection;
        inGameUiManager.UpdateCoinsCountTxt(coinsCollectedThisRound);
    }
}
