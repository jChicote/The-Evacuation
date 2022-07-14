using UnityEngine;

namespace TheEvacuation.Common
{

    public interface ICheckPaused
    {
        bool CheckIsPaused();
    }

    public class BaseInteractiveObject : MonoBehaviour, IPausable
    {

        #region - - - - - - Fields - - - - - -

        [SerializeField]
        public bool isPaused = false;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public bool IsPaused { get => isPaused; set => isPaused = value; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public virtual void OnPauseEntity()
            => isPaused = true;

        public virtual void OnUnpauseEntity()
            => isPaused = false;

        #endregion Methods

    }

}