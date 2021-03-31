using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoadSettings
{
    void CheckPreviousSave();
    void SaveSettings();
}

namespace UserInterface
{
    public class SettingsMenu : MonoBehaviour
    {
        public float settingsData;

        /// <summary>
        /// Checks and gets the previous state if existing in memory.
        /// </summary>
        public void CheckPreviousSave()
        {

        }

        /// <summary>
        /// Saves settings data to memory
        /// </summary>
        public void SaveSettings()
        {

        }
    }

    public class SettingsParameters
    {
        // Video Settings


        // Audio Settings
        public float volume;
        public float vfxVolume;
        public float musicVolume;
    }
}
