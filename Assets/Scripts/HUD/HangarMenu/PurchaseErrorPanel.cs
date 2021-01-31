using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UserInterfaces
{
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