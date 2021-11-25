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
        void OnPlayerHasLanded(bool hasLanded);
    }

    public abstract class BaseEnemyController : MonoBehaviour, IEnemyController, IStatePatternSetter
    {
        public abstract void InitialiseController();
        public virtual void SetEntryState(SpawnPattern pattern) { }
        public virtual void OnPlayerHasLanded(bool hasLanded) { }
    }
}
