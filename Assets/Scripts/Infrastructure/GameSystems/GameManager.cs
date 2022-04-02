using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class GameManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public static GameManager instance { get; private set; }

        public SessionDataFacade SessionData;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        #endregion MonoBehaviour

    }

}
