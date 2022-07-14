using UnityEngine;

namespace TheEvacuation.PlayerSystems.Input
{
    public interface IMobileInputControlAdapter : IPlayerInputEnabler
    {
        void InitialiseInput();
    }

    public class MobileInputControlAdapter : MonoBehaviour, IMobileInputControlAdapter
    {
        public void InitialiseInput()
        {
            throw new System.NotImplementedException();
        }

        public void EnableInputOperation(bool enabled)
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
