using TheEvacuation.Model.ViewModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.MenuInterfaces.ShipSelection
{

    public interface IShipSelectionCellView
    {

        #region - - - - - - Methods - - - - - -

        void PopulateListCell(ShipCellModel shipModel, ShipSelectionController controller);

        #endregion Methods

    }

    public class ShipSelectionCellView : MonoBehaviour, IShipSelectionCellView
    {

        #region - - - - - - Fields - - - - - -

        public ShipSelectionController controller;
        public Button cellButton;
        public Image cellImage;
        public TMP_Text cellName;
        public int identifier;

        #endregion Fields

        #region - - - - - - MonoBehaviours - - - - - -

        #endregion MonoBehaviours

        #region - - - - - - Methods - - - - - -

        public void PopulateListCell(ShipCellModel shipModel, ShipSelectionController controller)
        {
            this.controller = controller;
            this.identifier = shipModel.identifier;
            cellImage.sprite = shipModel.shipSprite;
            cellName.text = shipModel.name;
        }

        public void OnCellSelection()
            => controller.OnShipCellSelection(this.identifier);

        #endregion Methods

    }

}
