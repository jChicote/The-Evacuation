using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.Level.SpawnManagement;

namespace Evacuation.Actor.EnemySystems
{
    public interface IEnemyController
    {
        void InitialiseController();
    }

    public interface IStatePatternSetter
    {
        void SetEntryState(SpawnPattern pattern);
    }

    public abstract class BaseEnemyController : MonoBehaviour, IEnemyController
    {
        public abstract void InitialiseController();
        public virtual void SetEntryState(SpawnPattern pattern) { }
    }
}
