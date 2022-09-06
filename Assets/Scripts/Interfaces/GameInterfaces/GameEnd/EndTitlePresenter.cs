using System.Collections;
using TMPro;
using UnityEngine;

namespace TheEvacuation.GameEnd
{

    public class EndTitlePresenter : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField]
        private TMP_Text endTitleText;
        [Range(0f, 1f)]
        public float animationSpeed = 0.5f;

        private Color textColor;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Start()
            => textColor = endTitleText.color;

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        public void ToggleTextAnimation()
        {
            print("Did start event");
            StartCoroutine(TweenTextToOpaque());
        }

        public IEnumerator TweenTextToOpaque()
        {
            float currentAlpha = 0;
            float verticalRefVelocity = 0;
            textColor = endTitleText.color;

            while (Mathf.Round(Mathf.Abs(currentAlpha * 100)) < 100)
            {
                print(currentAlpha);
                currentAlpha = Mathf.SmoothDamp(currentAlpha, 1, ref verticalRefVelocity, animationSpeed);
                textColor.a = currentAlpha;
                endTitleText.color = textColor;
                yield return null;
            }
        }

        #endregion Methods

    }

}
