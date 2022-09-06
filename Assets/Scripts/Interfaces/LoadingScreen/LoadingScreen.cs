using System.Collections;
using TheEvacuation.Infrastructure.GameSystems.SceneSystems;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheEvacuation.Interfaces.LoadingScreen
{

    public class LoadingScreen : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public SceneLoader sceneLoader;
        public Slider loadingBar;
        public TMP_Text text;

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public IEnumerator LoadSceneAsync(string levelName)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

            while (!op.isDone)
            {
                float progress = Mathf.Clamp01(op.progress / 0.9f);

                loadingBar.value = progress;
                text.text = progress * 100f + "%";

                yield return null;
            }
        }

        #endregion Methods

    }

}
