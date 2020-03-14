using System.Collections;
using System.Collections.Generic;
using LukasScripts.AI;
using UnityEngine;


//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



    public class EnemyAttackState : PlayerBaseState
    {

        private float currentTime;
        private int rand;

        public override void EnterState(EnemyController_FSM enemy)
        {
            SoundManager.instance.PlayChaseMusic();
            enemy.AI.speed = 0;
            currentTime = enemy.waitBeforeAttack;
        }

        public override void OnCollisionEnter(EnemyController_FSM enemy)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(EnemyController_FSM enemy)
        {
            enemy.AI.isStopped = true;
            enemy.AI.velocity = Vector3.zero;
            enemy.anim.SetBool(TagManager.Run, false);
            enemy.AI.speed = 0;

            currentTime += Time.deltaTime;

            if(currentTime >= enemy.waitBeforeAttack)
            {
                currentTime = 0;
                PlayRandomAttack(enemy);
            }

            if (Vector3.Distance(enemy.transform.position, enemy.PlayerPosition.position) > enemy.attackRadius)
                enemy.TransitionToState(enemy.chaseState);
        }

        private void PlayRandomAttack(EnemyController_FSM enemy)
        {
             rand = Random.Range(0, 3);
        
            if (rand == 0)
                enemy.anim.SetTrigger(TagManager.Attack_1);
            else if (rand == 1)
                enemy.anim.SetTrigger(TagManager.Attack_2);
            else if (rand == 2)
                enemy.anim.SetTrigger(TagManager.Attack_3);
          
        }
    }

}