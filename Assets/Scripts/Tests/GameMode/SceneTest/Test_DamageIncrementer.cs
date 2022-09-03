using TheEvacuation.Common;
using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using TheEvacuation.Infrastructure.Score.UpdateScoreRecord;
using UnityEngine;

public class Test_DamageIncrementer : MonoBehaviour, IDamageable
{

    public void OnDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Has Hit");
        SceneScoreSystem scoreSystem = GameObject.FindObjectOfType<SceneScoreSystem>();
        scoreSystem.UpdateTotalScore(new ScoreEvent()
        {
            ScoreValue = 100,
            EventType = ScoreEventType.Hit,
            ScoreSubscriber = new ScoreSubscriber()
            {
                Name = gameObject.name,
                Transform = this.gameObject.transform
            }
        });
    }

}
