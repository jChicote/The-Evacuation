using UnityEngine;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class FlexPanelPresenter : MonoBehaviour, IFlexPanelPresenter
    {

        #region - - - - - - Fields - - - - - -

        public float targetHeight = 0;
        public float startingHeight = 0;
        public float targetWidth = 0;
        public float startWidth = 0;

        public bool useRectTransformDimensions = true;

        public Image panelImage;
        protected RectTransform panelTransform;
        protected IFlexPanelTweenAnimator panelAnimator;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public float Height { get; set; }
        public float Width { get; set; }

        #endregion Properties

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
        {
            this.panelTransform = this.GetComponent<RectTransform>();
            this.panelAnimator = this.GetComponent<IFlexPanelTweenAnimator>();

            this.panelAnimator.InitialiseFlexPanelTweenAnimator(this);

            if (useRectTransformDimensions)
            {
                targetHeight = panelTransform.rect.height;
                targetWidth = panelTransform.rect.width;
            }
        }

        private void OnEnable()
            => OnEnablePanel();

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void ClosePanel()
        {
            panelImage.enabled = false;
            this.gameObject.SetActive(false);
        }

        public void OnDisablePanel()
        {
            if (!this.gameObject.activeInHierarchy || panelAnimator == null || panelImage == null)
                return;

            StartCoroutine(panelAnimator.TweenToTargetDimensions(targetHeight, startingHeight, targetWidth, startWidth, ClosePanel));
        }

        public void OnEnablePanel()
        {
            if (panelAnimator == null || panelImage == null)
                return;

            StartCoroutine(panelAnimator.TweenToTargetDimensions(startingHeight, targetHeight, startWidth, targetWidth, default));
            panelImage.enabled = true;
        }

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
