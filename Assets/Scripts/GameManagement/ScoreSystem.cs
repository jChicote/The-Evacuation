using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IScoreEventAssigner
{
    ScoreEvent GetScoreEvent();
}

public class ScoreSystem : MonoBehaviour, IScoreEventAssigner
{
    [Header("Score Update Event")]
    public ScoreEvent OnScoreUpdate = new ScoreEvent();

    private ScoreData scoreData;

    private Timer hitTimer;

    public void InitialiseScoreSystem(ScoreData scoreData)
    {
        this.scoreData = scoreData;

        OnScoreUpdate.Invoke(this.scoreData);

        hitTimer = this.GetComponent<Timer>();
        hitTimer.SetTimer(5f);
    }

    /// <summary>
    /// Assigns method listener for score broadcast
    /// </summary>
    public ScoreEvent GetScoreEvent()
    {
        return OnScoreUpdate;
    }

    /// <summary>
    /// Increases the hit count of the player's projectiles
    /// </summary>
    public void IncrementHitCount()
    {
        hitTimer.enabled = true;
        OnScoreUpdate.Invoke(this.scoreData);
        hitTimer.ResetTimer();
        hitTimer.StartTimer();
    }

    /// <summary>
    /// Increases count of enemy death from player
    /// </summary>
    public void IncrementKIllCount(int killValue)
    {
        scoreData.totalKills++;
        OnScoreUpdate.Invoke(this.scoreData);
    }

    /// <summary>
    /// Increases the death count od the player's ship
    /// </summary>
    public void IncrementDeathCount()
    {
        scoreData.totalDeaths++;
        OnScoreUpdate.Invoke(this.scoreData);
    }

    /// <summary>
    /// Increasess the reecue count during scene game session
    /// </summary>
    public void IncrementRescueCount()
    {
        scoreData.totalRescued++;
        OnScoreUpdate.Invoke(this.scoreData);
    }

    /// <summary>
    /// Increments the score amount after kill
    /// </summary>
    public void IncrementScoreAmount(int amount)
    {
        scoreData.earnedScore += amount;
        OnScoreUpdate.Invoke(this.scoreData);
    }

    /// <summary>
    /// Manual updator on all label UI
    /// </summary>
    public void ForceLabelUpdate()
    {
        OnScoreUpdate.Invoke(this.scoreData); ;
    }

    /// <summary>
    /// Resets the hit count after timer has completed
    /// </summary>
    public void ResetHitCount()
    {
        hitTimer.enabled = false;
        scoreData.hitCount = 0;
        OnScoreUpdate.Invoke(this.scoreData);
    }

    /// <summary>
    /// Resets the score value during gameplay
    /// </summary>
    public void ResetScore()
    {
        scoreData.earnedScore = 0;
        OnScoreUpdate.Invoke(this.scoreData);
    }
}

[System.Serializable]
public class ScoreEvent : UnityEvent<ScoreData> { }

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

[System.Serializable]
public class LevelData
{
    // Level characteristics
    public bool isCompleted;
    public string levelID;
    public string levelName;

    // Level Constants
    public int maxRescuable;
    public float baseDifficulty;
    public float levelDuration;
    public int allowedLives;

    // Level Status
    public int topScore;
    public int totalRescued;
    public int totalKills;
    public int totalLives;

    public ScoreData ConvertToScoreData()
    {
        ScoreData newData = new ScoreData();
        newData.maxRescuable = maxRescuable;
        return newData;
    }

    public void UpdateLevelScore(ScoreData scoreData)
    {
        topScore = scoreData.earnedScore;
        totalRescued = scoreData.totalRescued;
    }
}
