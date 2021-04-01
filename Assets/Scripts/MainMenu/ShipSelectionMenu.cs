using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Evacuation.Session;

namespace Evacuation.UserInterface
{
    public interface IShipSelectionMenu
    {
        void InitialiseMenu(GameObject mainMenu);
        void PopulateMenu();
    }

    public class ShipSelectionMenu : MonoBehaviour, IShipSelectionMenu
    {
        [Header("Canvas Attributes")]
        public Button playButton;
        public GameObject contentView;

        private GameObject mainMenu;
        private List<GameObject> selectionCellList;

        public void InitialiseMenu(GameObject mainMenu)
        {
            this.mainMenu = mainMenu;

            selectionCellList = new List<GameObject>();
        }

        public void PopulateMenu()
        {
            ClearList();

            GameManager gameManager = GameManager.Instance;
            SessionData sessionData = SessionData.instance;
            GameObject selectionCellPrefab = gameManager.uiSettings.prototypeSelectionCell;
            GameObject cellInstance;
            ISelectionShipCell cellInterface;

            foreach (ShipAsset asset in gameManager.playerSettings.shipsList)
            {
                cellInstance = Instantiate(selectionCellPrefab, contentView.transform);
                cellInterface = cellInstance.GetComponent<ISelectionShipCell>();
                cellInterface.SetCell(asset.stringID, asset.image);
                selectionCellList.Add(cellInstance);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartGamePlay()
        {
            ISceneLoad sceneLoad = GameManager.Instance.sceneLoader.GetComponent<ISceneLoad>();
            sceneLoad.LoadLevel(1);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearList()
        {
            if (selectionCellList.Count == 0) return;

            for (int i = 0; i < selectionCellList.Count; i++)
            {
                Destroy(selectionCellList[i]);
            }

            selectionCellList.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReturnToHome()
        {
            mainMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}