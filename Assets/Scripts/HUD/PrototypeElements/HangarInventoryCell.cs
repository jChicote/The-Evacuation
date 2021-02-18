using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class HangarInventoryCell : InventoryListCell
    {
        [Header("Action UI")]
        public GameObject actionGroup;
        public Button informationButton;
        public Button sellButton;

        private bool isActionsVisible = false;

        public void RevealActionGroup()
        {
            isActionsVisible = !isActionsVisible;
            actionGroup.SetActive(isActionsVisible);
        }

        public void SellItem()
        {
            Debug.Log("Sold Item");
        }

        public override void RevealInformation()
        {
            infoPanelInterface.SetInfoPanel(this.equipmentID);
        }
    }

}