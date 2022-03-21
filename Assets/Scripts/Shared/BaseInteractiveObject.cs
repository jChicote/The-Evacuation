using UnityEngine;

namespace TheEvacuation.Shared
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

        public virtual void OnPause()
        {
            isPaused = true;
        }

        public virtual void OnUnpause()
        {
            isPaused = false;
        }

        #endregion Methods

    }

}