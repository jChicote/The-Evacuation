using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject informationPanel;

    public List<GameObject> displayedCells;

    [Header("Action Items")]
    public GameObject actionGroup;

    public void InitialiseMenu()
    {

    }

    public void PopulateInventoryList()
    {
        List<string> indentifierList = new List<string>();

        SortListItems(indentifierList);
    }

    public void SortListItems(List<string> indentifierList)
    {
        //Function currently sorts based on type than ranking
        // Gets all string in hangar weapons and then assigns to list strings ordered from their respective tpye
    }


    
}
