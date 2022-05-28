using TheEvacuation.Common;
using TheEvacuation.Infrastructure.GameSystems;
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
        scoreSystem.UpdateTotalScore(100);
    }

}
