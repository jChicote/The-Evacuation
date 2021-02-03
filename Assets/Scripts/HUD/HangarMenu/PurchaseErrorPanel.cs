using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserInterfaces
{
    /// <summary>
    /// Responsible for handling the actions of the purchase panel on the shop menu.
    /// </summary>
    public class PurchaseErrorPanel : MonoBehaviour
    {
        public void HideMessageBox()
        {
            gameObject.SetActive(false);
        }

        public void OpenBank()
        {
            Debug.Log("Opens bank");
            gameObject.SetActive(false);
        }
    }

}