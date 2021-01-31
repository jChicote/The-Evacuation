using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IShipCell
{
    void SetCell(string stringID, Sprite image);

}

public class ShipListCell : MonoBehaviour
{
    [Header("Cell Attributes")]
    public Image cellThumbnail;

    private string stringID;
    private Sprite imageThumbnail;

    public void SetCell(string stringID, Sprite image)
    {
        this.stringID = stringID;
        this.imageThumbnail = image;
    }

    private void PopulateCell()
    {
        cellThumbnail.sprite = imageThumbnail;
    }

    public void OpenSelectedShip()
    {
        Debug.Log("Ship pressed triggered");
    }
    
}
