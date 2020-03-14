using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LukasScripts.AI;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts.AI{

    public enum EnemyStates { Idle,Patrol,Chase,Attack}

	public class EnemyController_FSM : MonoBehaviour
	{
        private NavMeshAgent ai;
        [HideInInspector]
        public Animator anim;

        private PlayerBaseState currentState;
        private EnemyStates enemyCurState;

        public readonly EnemyIdleState idleState = new EnemyIdleState();
        public readonly EnemyPatrolState patrolState = new EnemyPatrolState();
        public readonly EnemyChaseState chaseState = new EnemyChaseState();
        public readonly EnemyAttackState attackState = new EnemyAttackState();


        public float walkSpeed = 3f;
        public float chaseSpeed = 6f;
        public float patrolRadius = 15f;
        public float chaseRadius = 20f;
        public float attackRadius = 6f;
        public float waitBeforeAttack = 2f;

        private AudioSource audioS;
        [SerializeField] private AudioClip[] sounds;

        private Transform playerPos;

        private void Start()
        {
            ai = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            audioS = GetComponent<AudioSource>();
            playerPos = GameObject.FindGameObjectWithTag(TagManager.Player).transform;
            PlayRandomSound();
            TransitionToState(idleState);
            
        }
        private void PlayRandomSound()
        {
            var rand = sounds[Random.Range(0, sounds.Length)];
            audioS.clip = rand;
            audioS.Play();
        }
        private void Update()
        {
            currentState.Update(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            currentState.OnCollisionEnter(this);
        }

        public void TransitionToState(PlayerBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }

        public Transform PlayerPosition
        {
            get => playerPos;
        }

        public NavMeshAgent AI
        {
            get => ai;
            set => ai = value;
        }


        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawWireSphere(transform.position, patrolRadius);

        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireSphere(transform.position, chaseRadius);

        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, attackRadius);
        //}
    }


}