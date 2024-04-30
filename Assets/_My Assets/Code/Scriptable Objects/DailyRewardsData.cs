using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Daily Reward Data", menuName = "Daily Reward Data")]
public class DailyRewardsData : ScriptableObject
{
    public int dayCount = 0;
    public int lastRecordedDate;
    public int currentDate;

    public RewardsList rewardList;

    public Color activeColor;
    public Color normalColor;
}

[System.Serializable]
public class RewardsList
{
    public List<Reward> rewards = new List<Reward>();
}

[System.Serializable]
public class Reward
{
    public Sprite giftSprite;
    public string dayName = string.Empty;
    public int coins = 0;
}
