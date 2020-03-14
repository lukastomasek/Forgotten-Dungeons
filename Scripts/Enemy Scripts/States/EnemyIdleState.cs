using System.Collections;
using System.Collections.Generic;
using LukasScripts.AI;
using UnityEngine;
using UnityEngine.AI;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{

    public class EnemyIdleState : PlayerBaseState
    {
        private EnemyStates states;
        public float patrolTimer = 5f;
        private float currentTimer = 0f;
        private Vector3 newDestination;

        public override void EnterState(EnemyController_FSM enemy)
        {
            states = EnemyStates.Idle;
            enemy.anim.Play(TagManager.Idle);
        }

        public override void OnCollisionEnter(EnemyController_FSM enemy)
        {
           
        }

        public override void Update(EnemyController_FSM enemy)
        {
            Patrol(enemy);

            if(Vector3.Distance(enemy.transform.position, enemy.PlayerPosition.position) <= enemy.patrolRadius)
            {
                states = EnemyStates.Chase;
                enemy.TransitionToState(enemy.chaseState);
            }
        }


        private void Patrol(EnemyController_FSM enemy)
        {
            currentTimer += Time.deltaTime;
            enemy.AI.speed = enemy.walkSpeed;

            if(currentTimer > patrolTimer)
            {
                currentTimer = 0f;
                SetNewDestination(enemy);
            }

            if (enemy.AI.velocity.sqrMagnitude > 0)
                enemy.anim.SetBool(TagManager.Walk, true);
            else
                enemy.anim.SetBool(TagManager.Walk, false);

        }

        private void SetNewDestination(EnemyController_FSM enemy)
        {
            newDestination = RandomNavSphere(enemy.transform.position, enemy.patrolRadius, -1);
            enemy.AI.SetDestination(newDestination);
        }

        private Vector3 RandomNavSphere(Vector3 origin, float distance, int layer)
        {
            Vector3 randDir = Random.insideUnitSphere * distance;
            randDir += origin;
            NavMeshHit hit;

            NavMesh.SamplePosition(randDir, out hit, distance, layer);

            return hit.position;
            
        }
    }

}