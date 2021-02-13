using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserInterfaces.HUD
{
    public interface IHudInitialiser
    {
        void InitialiseHud();
    }

    public interface IHudAccessors
    {
        IVitalityBar GetHealthBar();
        IVitalityBar GetShieldBar();

    }

    public class PlayerHUDManager : MonoBehaviour, IHudInitialiser, IHudAccessors
    {
        public GameObject vitalComponents;
        public GameObject centalComponents;

        [Space]
        [SerializeField] private VitalityBarComponent healthBar;
        [SerializeField] private VitalityBarComponent shieldBar;

        public void InitialiseHud()
        {

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
    }
}
