using UnityEngine;

namespace TheEvacuation.Common
{

    public interface IGameHandler
    {

        #region - - - - - - Methods - - - - - -

        object AwakeHandle(object request);

        IGameHandler SetNext(IGameHandler nextHandler);

        #endregion Methods

    }

    public abstract class GameHandler : MonoBehaviour, IGameHandler
    {

        #region - - - - - - Fields - - -  - - -

        private IGameHandler nextGameJobHandler;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public virtual object AwakeHandle(object request)
            => this.nextGameJobHandler != null ? this.nextGameJobHandler.AwakeHandle(request) : null;

        public IGameHandler SetNext(IGameHandler nextHandler)
        {
            this.nextGameJobHandler = nextHandler;
            return nextHandler;
        }

        #endregion Methods

    }

}
