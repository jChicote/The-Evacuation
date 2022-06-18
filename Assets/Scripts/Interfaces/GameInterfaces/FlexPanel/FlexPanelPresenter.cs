using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class FlexPanelPresenter : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public float targetHeight = 0;
        public float startingHeight = 0;
        public float targetWidth = 0;
        public float startWidth = 0;

        public bool useTransformDimensions = true;

        public Image panelImage;
        protected RectTransform panelTransform;
        protected FlexPanelTweenAnimator panelAnimator;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public float Height { get; set; }
        public float Width { get; set; }

        #endregion Properties

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            this.panelTransform = this.GetComponent<RectTransform>();
            this.panelAnimator = this.GetComponent<FlexPanelTweenAnimator>();

            this.panelAnimator.InitialiseFlexPanelTweenAnimator(this);

            if (useTransformDimensions)
            {
                Height = panelTransform.rect.height;
                Width = panelTransform.rect.width;
            }
        }

        private void OnEnable()
        {
            if (panelAnimator == null || panelImage == null)
                return;

            StartCoroutine(panelAnimator.TweenToTargetDimensions(startingHeight, targetHeight, startWidth, targetWidth));
            panelImage.enabled = true;
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
