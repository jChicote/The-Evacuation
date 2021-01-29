using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject hangarMenu;

    public void Start()
    {
        hangarMenu.GetComponent<HangarMenu>().InitialiseHangar();
    }

    public void OnPlay()
    {
        ISceneLoad sceneLoad = GameManager.Instance.sceneLoader.GetComponent<ISceneLoad>();
        sceneLoad.LoadLevel(1);
    }

    public void RevealHanger()
    {
        Debug.Log("Reveal Hanger");
        mainMenu.SetActive(false);
        hangarMenu.SetActive(true);
    }

    public void OnSetting()
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
