using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorTracker
{
    void RegisterFriendlyEntity(GameObject actor);
    void RegisterEnemyEntity(GameObject actor);
    int GetFriendlyCount();
    int GetEnemyCount();
    List<GameObject> GetAllFrienlyActors();
    List<GameObject> GetAllEnemyActors();
    GameObject GetRandomEnemyEntity();
    GameObject GetRandomFriendly();
}

public class SceneActorTracker : MonoBehaviour, IActorTracker
{
    // Fields
    private List<GameObject> friendlyActors = new List<GameObject>();
    private List<GameObject> enemyActors = new List<GameObject>();

    public void RegisterFriendlyEntity(GameObject actor)
    {
        friendlyActors.Add(actor);
    }

    public void RegisterEnemyEntity(GameObject actor)
    {
        enemyActors.Add(actor);
    }

    public int GetFriendlyCount() { return friendlyActors.Count; }
    public int GetEnemyCount() { return enemyActors.Count; }

    public List<GameObject> GetAllFrienlyActors() { return friendlyActors; }
    public List<GameObject> GetAllEnemyActors() { return enemyActors; }

    public GameObject GetRandomEnemyEntity()
    {
        return enemyActors[Random.Range(0, enemyActors.Count)];
    }

    public GameObject GetRandomFriendly()
    {
        return friendlyActors[Random.Range(0, friendlyActors.Count)];
    }
}
