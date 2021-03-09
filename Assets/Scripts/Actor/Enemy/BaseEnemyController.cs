using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evacuation.Actor.EnemySystems
{
    public interface IEnemyController
    {
        void InitialiseController();
    }

    public abstract class BaseEnemyController : MonoBehaviour, IEnemyController
    {
        public abstract void InitialiseController();
    }
}
