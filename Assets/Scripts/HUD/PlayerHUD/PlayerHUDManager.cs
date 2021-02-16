using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterfaces.HUD
{
    public interface IHudInitialiser
    {
        void InitialiseHud(IScoreEventAssigner scoreAssigner);
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

        [Space]
        [SerializeField] private GameObject[] scoreLabel;

        public void InitialiseHud(IScoreEventAssigner scoreAssigner)
        {
            foreach (GameObject label in scoreLabel)
            {
                label.GetComponent<IScoreTextUpdater>().SetScoreListener(scoreAssigner);
            }
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
