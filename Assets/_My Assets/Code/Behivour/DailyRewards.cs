using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

public enum DailyRewardStatus
{
    NOT_COLLECTED,
    COLLECTED
}

public class DailyRewards : MonoBehaviour
{
    UiManager uiManager;
    EconomyManager economyManager;
    [SerializeField] private DailyRewardStatus dailyRewardStatus;
    [SerializeField] private DailyRewardsData dailyRewardsData;

    [Header("<size=15>[USER INTERFACE]")]
    [SerializeField] private TMP_Text rewardCollectedTxt;
    [SerializeField] private List<RewardCell> rewardCellsList = new List<RewardCell>(); 


    private void Start()
    {
        uiManager = UiManager.instance;
        economyManager = EconomyManager.instance;   

        dailyRewardsData.dayCount = PlayerPrefs.GetInt(ConstantKeys.REWARD_COUNT, 0);
        dailyRewardsData.currentDate = Convert.ToInt16(DateTime.Now.Date.ToString("dd"));
        dailyRewardsData.lastRecordedDate = PlayerPrefs.GetInt(ConstantKeys.LAST_DATE, 0);

        if (dailyRewardsData.lastRecordedDate != dailyRewardsData.currentDate)
        {
            dailyRewardStatus = DailyRewardStatus.NOT_COLLECTED;
        }
        else
        {
            dailyRewardStatus = DailyRewardStatus.COLLECTED;
        }

        foreach (RewardCell rewardCell in rewardCellsList)
        {
            rewardCell.InitCells();
        }
    }

    public DailyRewardStatus GetDailyRewardStatus()
    {
        return dailyRewardStatus;
    }

    public void CollectBtn()
    {
        PlayerPrefs.SetInt(ConstantKeys.LAST_DATE, dailyRewardsData.currentDate);
        int rewardCount = PlayerPrefs.GetInt(ConstantKeys.REWARD_COUNT, 0);
        economyManager.AddCoins(dailyRewardsData.rewardList.rewards[rewardCount].coins);

        //<b>50,000</b> <color=#898A8A>Coins Collected
        //.ToString("#,##0", CultureInfo.InvariantCulture);
        int coinToShow = dailyRewardsData.rewardList.rewards[rewardCount].coins;
        rewardCollectedTxt.text = $"<b>{coinToShow}</b> <color=#898A8A>Coins collected.";

        rewardCount += 1;
        PlayerPrefs.SetInt(ConstantKeys.REWARD_COUNT, rewardCount);

        uiManager.OpenCanvas(CanvasCellsName.DAILY_REWARD_COLLECTED);

    }
}
