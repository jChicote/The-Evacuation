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
        public SessionData sessionData;
        public UserInterfaceFlyweightSettings interfacesettings;
        public PlayerFlyweightSettings playerSettings;
        public int shipIndentifier;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ShipSelectionController(
            PlayerFlyweightSettings playerSettings,
            SessionData sessionData,
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
            if (sessionData.CurrentPlayer.spaceShipHanger == null || sessionData.CurrentPlayer.spaceShipHanger.Count == 0)
                return;

            foreach (var ship in sessionData.CurrentPlayer.spaceShipHanger)
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
            sessionData.SelectedSpaceShip = sessionData.CurrentPlayer.spaceShipHanger
                                                .Where(ss => ss.identifier == shipIndentifier)
                                                .AsEnumerable()
                                                .SingleOrDefault()
                                                .Clone();

            Debug.Log("Selected Ship: " + sessionData.SelectedSpaceShip.name);
        }

        #endregion Methods

    }

}