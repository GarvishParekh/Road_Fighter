using UnityEngine;

[CreateAssetMenu(fileName = "Score Data", menuName = "Score Data")]
public class ScoreData : ScriptableObject
{
    public scoreMultiplier[] scoreMultiplier;
    
    public float scoreCount = 0;
    public int currentScoreLevel = 0;

    public float scoreMultiplierValue = 0;
    public float increasingValue = 0.5f;
}

[System.Serializable]
public class scoreMultiplier
{
    public string levelDisplayName;
    public float value;
    public Color barColor;
}