using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Model
{
    public class UTestSaveLoader : MonoBehaviour
    {
        public bool isRunning = false;

        // Update is called once per frame
        void Update()
        {
            if (!isRunning) return;

            SessionData.instance.Save();
            isRunning = false;
        }
    }
}
