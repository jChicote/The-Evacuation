using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheEvacuation.Infrastructure.GameSystems
{

    public class SceneLoader : MonoBehaviour
    {

        #region - - - - - - Methods - - - - - -

        public void LoadNextScene(string sceneName)
            => SceneManager.LoadScene(sceneName);

        #endregion Methods

    }

}
