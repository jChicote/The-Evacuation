using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Evacuation.UserInterface
{
    public class HangarInventoryCell : InventoryListCell
    {
        [Header("Action UI")]
        public Button informationButton;
        public Button sellButton;

        public void SellItem()
        {
            Debug.Log("Sold Item");
        }

        public override void RevealInformation()
        {
            informationPanel.SetInfoPanel(this.instanceID);
        }
    }

}