using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.UserInterface.HUD
{
    public interface IHudInitialiser
    {
        void InitialiseHud(IScoreEventAssigner scoreAssigner, LevelData levelData, SceneController sceneController);
    }

    public interface IHudAccessors
    {
        IVitalityBar GetHealthBar();
        IVitalityBar GetShieldBar();

    }

    public class PlayerHUDManager : MonoBehaviour, IHudInitialiser, IHudAccessors, IPausable
    {
        public GameObject vitalComponents;
        public GameObject centalComponents;

        [Space]
        [SerializeField] private VitalityBarComponent healthBar;
        [SerializeField] private VitalityBarComponent shieldBar;
        [SerializeField] private GameTimerBar gameTimerBar;

        [Space]
        [SerializeField] private GameObject[] scoreLabel;

        public void InitialiseHud(IScoreEventAssigner scoreAssigner, LevelData levelData, SceneController sceneController)
        {
            foreach (GameObject label in scoreLabel)
            {
                label.GetComponent<IScoreTextUpdater>().SetScoreListener(scoreAssigner);
            }

            gameTimerBar.InitialiseTimerBar(sceneController, levelData.levelDuration);
        }

        /// <summary>
        /// Accessor for the health bar interface
        /// </summary>
        public IVitalityBar GetHealthBar()
        {
            return healthBar.GetComponent<IVitalityBar>();
        }

        /// <summary>
        /// Accessor for shield bar interface
        /// </summary>
        public IVitalityBar GetShieldBar()
        {
            return shieldBar.GetComponent<IVitalityBar>();
        }

        public void OnPause()
        {
            vitalComponents.SetActive(false);
            centalComponents.SetActive(false);
        }

        public void OnUnpause()
        {
            vitalComponents.SetActive(true);
            centalComponents.SetActive(true);
        }
    }
}
