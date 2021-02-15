using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private ScoreData scoreData;

    private Timer hitTimer;

    public void InitialiseScoreSystem()
    {
        hitTimer = this.GetComponent<Timer>();
        hitTimer.SetTimer(5f);
    }

    public void SetLevelBeginData()
    {

    }

    /// <summary>
    /// Increases the hit count of the player's projectiles
    /// </summary>
    public void IncrementHitCount()
    {
        hitTimer.enabled = true;
        hitTimer.ResetTimer();
        hitTimer.StartTimer();
    }

    /// <summary>
    /// Increases count of enemy death from player
    /// </summary>
    public void IncrementKIllCount(int killValue)
    {

    }

    /// <summary>
    /// Increases the death count od the player's ship
    /// </summary>
    public void IncrementDeathCount()
    {

    }

    /// <summary>
    /// Increasess the reecue count during scene game session
    /// </summary>
    public void IncrementRescueCount()
    {

    }

    /// <summary>
    /// Increments the score amount after kill
    /// </summary>
    public void IncrementScoreAmount()
    {

    }

    /// <summary>
    /// Resets the hit count after timer has completed
    /// </summary>
    public void ResetHitCount()
    {
        hitTimer.enabled = false;
    }

    /// <summary>
    /// Resets the score value during gameplay
    /// </summary>
    public void ResetScore()
    {
        
    }
}

[System.Serializable]
public struct ScoreData
{
    // Score related variables
    public int earnedScore;

    // Life score related variables
    public int totalKills;
    public int totalDeaths;
    
    // Rescure related variables
    public int totalRescued;
    public int maxRescuable;

    public int hitCount;
}
