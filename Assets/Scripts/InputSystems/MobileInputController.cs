using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheEvacuation.InputSystem
{
    public interface IMobileInputController: IInputToggling
    {
        void InitialiseInput();
    }

    public class MobileInputController : MonoBehaviour, IMobileInputController
    {
        public void InitialiseInput()
        {
            throw new System.NotImplementedException();
        }

        public void ToggleInputActivation(bool enabled)
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
