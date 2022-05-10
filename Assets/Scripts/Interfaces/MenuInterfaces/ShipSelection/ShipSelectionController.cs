using System.Linq;
using TheEvacuation.Infrastructure.GameSystems;
using TheEvacuation.Model.Entities;
using TheEvacuation.Model.ViewModels;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Interfaces.MenuInterfaces.ShipSelection
{

    public class ShipSelectionController
    {

        #region - - - - - - Fields - - - - - -

        public ShipSelectionView view;
        public SessionDataFacade sessionData;
        public UserInterfaceFlyweightSettings interfacesettings;
        public PlayerFlyweightSettings playerSettings;
        public int shipIndentifier;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ShipSelectionController(
            PlayerFlyweightSettings playerSettings,
            SessionDataFacade sessionData,
            ShipSelectionView view,
            UserInterfaceFlyweightSettings interfaceSettings)
        {
            this.playerSettings = playerSettings;
            this.view = view;
            this.sessionData = sessionData;
            this.interfacesettings = interfaceSettings;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public void LoadShipSelectionList()
        {
            if (sessionData.Player.spaceShipHanger == null || sessionData.Player.spaceShipHanger.Count == 0)
                return;

            foreach (var ship in sessionData.Player.spaceShipHanger)
            {
                ShipAsset asset = playerSettings.GetShipAssetFromIdentifier(ship.identifier);
                view.CreateShipSelectionListCell(interfacesettings.shipSelectionCellPrefab, new ShipCellModel()
                {
                    identifier = ship.identifier,
                    name = ship.name,
                    shipSprite = asset.shipSprite
                });
            }
        }

        public void OnShipCellSelection(int identifier)
        {
            shipIndentifier = identifier;
            view.MakePlayButtonInteractable();
            Debug.Log("Ship has been selected.");
        }

        public void SetSelectedShip()
        {
            sessionData.SelectedSpaceShip = sessionData.Player.spaceShipHanger
                                                .Where(ss => ss.identifier == shipIndentifier)
                                                .AsEnumerable()
                                                .SingleOrDefault()
                                                .Clone();

            Debug.Log("Selected Ship: " + sessionData.SelectedSpaceShip.name);
        }

        #endregion Methods

    }

}