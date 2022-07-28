using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TheEvacuation.Utilities
{

    public class TimedTriggerEvent : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        [Space]
        public UnityEvent OnTimerCompletion;

        [Space]
        [Range(0f, 5f)]
        public float timerDuration = 1f;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        public void OnEnable()
        {
            StartCoroutine(TickUntilDurationEnd(timerDuration));
        }

        #endregion MonoBehaviour

        #region - - - - - - Methods - - - - - -

        private IEnumerator TickUntilDurationEnd(float duration)
        {
            yield return new WaitForSeconds(duration);
            OnTimerCompletion?.Invoke();
        }

        #endregion Methods

    }

}