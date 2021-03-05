using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject hangarMenu;
        public GameObject selectionMenu;

        private IShipSelectionMenu selectionMenuInterface;

        public void InitialiseMainMenu()
        {
            hangarMenu.GetComponent<HangarMenu>().InitialiseHangar();

            selectionMenu = Instantiate(GameManager.Instance.uiSettings.selectionMenuPrefab);
            selectionMenuInterface = selectionMenu.GetComponent<IShipSelectionMenu>();
            selectionMenuInterface.InitialiseMenu(this.gameObject);
        }

        public void OnPlay()
        {
            selectionMenu.SetActive(true);
            selectionMenuInterface.PopulateMenu();
            this.gameObject.SetActive(false);
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

}