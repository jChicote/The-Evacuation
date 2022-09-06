using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TheEvacuation.Utilities
{

    public class ActivityTimedTriggerEvent : TimedTriggerEvent
    {

        #region - - - - - - Fields - - - - - -

        [Space]
        public UnityEvent OnAwake;
        public UnityEvent OnTimedAction;

        [Range(0f, 1f)]
        public float endEventToggleTime = 0.5f;

        private bool hasToggledEnd = false;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        public void OnEnable()
        {
            base.OnEnable();
            OnAwake?.Invoke();
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        protected override IEnumerator TickUntilDurationEnd(float duration)
        {
            float currentDuration = 0;

            while (currentDuration < duration)
            {
                currentDuration += Time.deltaTime;

                if (!hasToggledEnd && ((currentDuration / duration) > endEventToggleTime))
                {
                    OnTimedAction?.Invoke();
                    hasToggledEnd = true;
                }

                yield return null;
            }

            OnTimerCompletion?.Invoke();
        }

        #endregion Methods

    }

}
