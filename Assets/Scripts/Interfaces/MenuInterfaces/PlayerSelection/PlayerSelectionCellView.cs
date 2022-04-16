using System;
using TheEvacuation.Model.ViewModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.MenuInterfaces.PlayerSelection
{

    public interface IPlayerSelectionCellView
    {

        #region - - - - - - Methods - - - - - -

        void PopulateListCell(PlayerCellModel model, SelectPlayerMenuController controller);

        #endregion Methods

    }

    public class PlayerSelectionCellView : MonoBehaviour, IPlayerSelectionCellView
    {

        #region - - - - - - Fields - - - - - -

        public Image avatarThumbnail;
        public TMP_Text nameText;
        public TMP_Text scoreText;
        public SelectPlayerMenuController parentController;
        public Guid playerID;

        #endregion Fields

        #region - - - - - - Monobehaviour - - - - - -

        private void Start()
        {

        }

        #endregion Monobehaviour

        #region - - - - - - Methods - - - - - -

        public void PopulateListCell(PlayerCellModel model, SelectPlayerMenuController controller)
        {
            this.avatarThumbnail.sprite = model.avatar;
            this.nameText.text = model.name;
            this.scoreText.text = model.score.ToString();
            this.playerID = model.id;
            this.parentController = controller;
        }

        public void OnCellSelection()
            => parentController.OnPlayerCellSelection(playerID);

        #endregion Methods

    }

}
