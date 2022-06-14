using UnityEngine;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class FlexPanelPresenter : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public float maxHeight = 0;
        public float minHeight = 0;
        public float maxWidth = 0;
        public float minWidth = 0;

        public bool useTransformDimensions = true;

        protected RectTransform panelTransform;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public float Height { get; set; }
        public float Width { get; set; }

        #endregion Properties

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            this.panelTransform = GetComponent<RectTransform>();

            if (useTransformDimensions)
            {
                Height = panelTransform.rect.height;
                Width = panelTransform.rect.width;
            }
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void SetAndUpdateDimensions(float height, float width)
        {
            Height = height;
            Width = width;

            panelTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height);
            panelTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Width);
        }

        public void UpdateTransformDimensions()
        {
            if (Height != null && Width != null)
            {
                Debug.LogError("Panel is missing Height and Width values");
                return;
            }

            panelTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height);
            panelTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Width);
        }

        #endregion Methods
    }

}
