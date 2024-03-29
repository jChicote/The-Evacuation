using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class GameManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public static GameManager Instance { get; private set; }

        public SessionData SessionData;
        public PlayerFlyweightSettings playerFlyweightSettings;
        public UserInterfaceFlyweightSettings userInterfaceFlyweightSettings;
        public SceneLoader sceneLoader;
        public SceneLevelManager sceneLevelManager;

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

            SessionData.Load();
        }

        #endregion MonoBehaviour

    }

}
