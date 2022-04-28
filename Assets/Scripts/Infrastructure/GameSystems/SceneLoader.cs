using TheEvacuation.Interfaces.LoadingScreen;
using TheEvacuation.ScriptableObjects.FlyweightSettings;
using UnityEngine;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class SceneLoader : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public UserInterfaceFlyweightSettings settings;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public void LoadNextScene(string sceneName)
            => StartCoroutine(Instantiate(settings.loadingScreenPrefab, transform.position, Quaternion.identity)
                                            .GetComponent<LoadingScreen>()
                                            .LoadSceneAsync(sceneName));

        #endregion Methods

    }

}
