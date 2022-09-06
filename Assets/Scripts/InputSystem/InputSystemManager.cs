using UnityEngine;
using UnityEngine.InputSystem;

namespace TheEvacuation.InputSystem
{

    public class InputSystemManager : MonoBehaviour
    {

        #region - - - - - - Fields - - - - - -

        public PlayerInput playerInput;

        #endregion Fields

        #region - - - - - - MonoBehaviour - - - - - -

        private void Awake()
        {
            playerInput = this.playerInput ?? this.GetComponent<PlayerInput>();
            playerInput.SwitchCurrentActionMap("Gameplay");
        }

        #endregion MonoBehaviour

    }

}
