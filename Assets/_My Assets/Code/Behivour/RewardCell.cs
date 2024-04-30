using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardCell : MonoBehaviour
{
    [SerializeField] private DailyRewardsData dailyRewardsData;
    int myIndex;
    [SerializeField] private Image giftImg;
    [SerializeField] private TMP_Text dayTxt;
    [SerializeField] private GameObject collectedImg;

    public void InitCells()
    {
        collectedImg.SetActive(false);
        myIndex = transform.GetSiblingIndex();

        Reward myReward = dailyRewardsData.rewardList.rewards[myIndex];
        giftImg.sprite = myReward.giftSprite;
        dayTxt.text = myReward.dayName;

        if (dailyRewardsData.dayCount == myIndex)
        {
            GetComponent<Image>().color = dailyRewardsData.activeColor;
        }
        else
        {
            GetComponent<Image>().color = dailyRewardsData.normalColor;
            if (dailyRewardsData.dayCount > myIndex)
            {
                collectedImg.SetActive(true);
            }
        }
    }
}
