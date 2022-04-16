using TheEvacuation.Model.Entities;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class GameManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public static GameManager Instance { get; private set; }

        public SessionDataFacade SessionData;

        public Player activePlayer;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        #endregion MonoBehaviour

    }

}
