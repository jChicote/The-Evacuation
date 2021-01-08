using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnPlay()
    {
        ISceneLoad sceneLoad = GameManager.Instance.sceneLoader.GetComponent<ISceneLoad>();
        sceneLoad.LoadLevel(1);
    }

    public void RevealHanger()
    {
        Debug.Log("Reveal Hanger");
    }

    public void OnSettings()
    {
        Debug.Log("Reveal Setting");
    }

    public void OnExit()
    {
        Debug.Log("Reveal Hanger");
    }

    public void OnClose()
    {
        GameObject.Destroy(this.gameObject);
    }
}
