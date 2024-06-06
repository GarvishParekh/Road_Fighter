using UnityEngine;

[CreateAssetMenu(fileName = "Score Data", menuName = "Score Data")]
public class ScoreData : ScriptableObject
{
    [Header (" <size=15>[ SCORE VALUES ] ")]
    public float scoreCount = 0;
    public int currentScoreLevel = 0;
    public float timeSpent = 0;

    [Space]
    public float scoreMultiplierValue = 0;
    public float increasingValue = 0.5f;

    [Space]
    public float userScoreLevel = 2;
    
    [Header (" <size=15>[ SCORE UI VALUE] ")]
    public scoreMultiplier[] scoreMultiplier;
    public Color textDefaultColor;
    public float shakeEffect = 200f;
}

[System.Serializable]
public class scoreMultiplier
{
    public string levelDisplayName;
    public float value;
    public Color barColor;
    public Color textColor;
}