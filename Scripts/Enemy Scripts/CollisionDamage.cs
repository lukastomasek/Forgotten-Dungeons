using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{

	public class CollisionDamage : MonoBehaviour
	{
        [SerializeField] private int damage = 10;
        [SerializeField] private float collisionRadius = 3f;

        private PlayerHealth playerHealth;
        [SerializeField] private ShakeCamera shakeCamera;

        private void Start()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            
        }

        private void Update()
        {
            Collider[] points = Physics.OverlapSphere(transform.position, collisionRadius);

            if(points.Length > 0)
            {
                playerHealth.ApplyDamage(damage);
            }
        }




        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawWireSphere(transform.position, collisionRadius);
        //}


    }

}