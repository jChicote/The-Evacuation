using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Evacuation.Actor.EnemySystems
{
    // !!! NOTICE !!!
    //
    // This state is called for discrete operations after entity death.
    // Known destroy methods are triggered by the animation state machine
    // under "DestroyActorOnExit" as a non-Monobehaviour class.
    public class EnemyDeathState : BaseComponentState
    {
        public override void BeginState()
        {
            Animator animator = this.GetComponentInChildren<Animator>();
            animator.SetBool("isDestroyed", true);

            Rigidbody2D enemyRB = this.GetComponent<Rigidbody2D>();
            enemyRB.velocity = Vector2.zero;
        }
    }
}
