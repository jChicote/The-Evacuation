using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace UserInterfaces.HUD
{
    public interface IScoreText
    {
        void UpdateScoreText(ScoreData scoreData);
    }

    public class ScoreBox : MonoBehaviour, IScoreText
    {
        public TextMeshProUGUI scoreText;

        public void InitialiseScorebox(IScoreEventAssigner eventAssigner)
        {

            // assigns method action to score event
            Debug.LogWarning("Called Warning");
            eventAssigner.GetScoreEvent().AddListener(UpdateScoreText);
        }

        /// <summary>
        /// Called to update the score text through triggerent unity events.
        /// </summary>
        public void UpdateScoreText(ScoreData scoreData)
        {
            Debug.LogWarning("Was Called");
            scoreText.text = scoreData.earnedScore.ToString();
        }
    }

}