using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.GameInterfaces.FlexPanel
{

    public class FlexPanelPresenter : MonoBehaviour, IFlexPanelPresenter
    {

        #region - - - - - - Fields - - - - - -

        public float startingHeight = 0;
        public float startWidth = 0;
        public float targetHeight = 0;
        public float targetWidth = 0;
        public bool useRectTransformDimensions = true;

        [Space]
        public Image panelImage;

        [Space]
        public UnityEvent OnPanelAwake;
        public UnityEvent OnPanelStart;
        public UnityEvent OnPanelExit;
        public UnityEvent OnPanelEnd;

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

            HandlePanelAwakeAction();
        }

        private void OnEnable()
            => HandlePanelAwakeAction();

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void AnimatePanelToOpen()
        {
            StartCoroutine(panelAnimator.TweenToTargetDimensions(startingHeight, targetHeight, startWidth, targetWidth, HandlePanelStartingAction));
            panelImage.enabled = true;
        }

        public void ClosePanel()
        {
            OnPanelEnd.Invoke();
            panelImage.enabled = false;
            this.gameObject.SetActive(false);
        }

        public virtual void HandlePanelAwakeAction()
        {
            if (panelAnimator == null || panelImage == null)
                return;

            OnPanelAwake.Invoke();
            SetAndUpdateDimensions(startingHeight, startWidth);
            AnimatePanelToOpen();
        }

        public virtual void HandlePanelExitAction()
        {
            if (!this.gameObject.activeInHierarchy || panelAnimator == null || panelImage == null)
                return;

            StartCoroutine(panelAnimator.TweenToTargetDimensions(targetHeight, startingHeight, targetWidth, startWidth, ClosePanel));
            OnPanelExit.Invoke();
        }

        public virtual void HandlePanelStartingAction()
            => OnPanelStart.Invoke();

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
