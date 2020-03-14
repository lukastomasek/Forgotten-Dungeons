using System.Collections;
using System.Collections.Generic;
using LukasScripts.AI;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{


    [System.Serializable]
    public class EnemyChaseState : PlayerBaseState
    {
        float t = 2f;
        EnemyStates states;
        public bool spotted = false;

        public override void EnterState(EnemyController_FSM enemy)
        {
            spotted = true;
            enemy.transform.LookAt(enemy.PlayerPosition);
            enemy.anim.SetTrigger(TagManager.Shout);
            enemy.AI.isStopped = true;
            enemy.AI.speed = 0;
        }

        public override void OnCollisionEnter(EnemyController_FSM enemy)
        {
            
        }

        public override void Update(EnemyController_FSM enemy)
        {
            enemy.AI.SetDestination(enemy.PlayerPosition.position);
            enemy.AI.speed = enemy.chaseSpeed;
            enemy.AI.isStopped = false;

            if (enemy.AI.velocity.sqrMagnitude > 0)
                enemy.anim.SetBool(TagManager.Run, true);
            else
                enemy.anim.SetBool(TagManager.Run, false);
           
            if(Vector3.Distance(enemy.transform.position, enemy.PlayerPosition.position) <= enemy.attackRadius)
            {
                states = EnemyStates.Attack;
                enemy.TransitionToState(enemy.attackState);
            }

        }
    }

}