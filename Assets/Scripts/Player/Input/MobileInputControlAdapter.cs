using UnityEngine;

namespace TheEvacuation.Player.Input
{
    public interface IMobileInputControlAdapter : IInputToggling
    {
        void InitialiseInput();
    }

    public class MobileInputControlAdapter : MonoBehaviour, IMobileInputControlAdapter
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
