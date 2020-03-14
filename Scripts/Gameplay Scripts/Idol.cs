using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class Idol : MonoBehaviour
	{
        [SerializeField] private float radius = 5f;

        private Transform player;
        private bool canActivate = false;
        private bool didInteract;
        [SerializeField] private GameObject fx;
        private Text interactTxt;
        private GameplayManager gameplayManager;
        public delegate void OnIdolActivated();
        public static event OnIdolActivated idolActivated;

        private void Awake()
        {
            gameplayManager = FindObjectOfType<GameplayManager>();
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag(TagManager.Player).transform;
            interactTxt = GetComponentInChildren<Text>();
           
        }

        private void Update()
        {
            if (gameplayManager.SecondAct == false)
                return;

            if (Vector3.Distance(transform.position, player.position) <= radius)
            {
                canActivate = true;
                
            }
            else
                canActivate = false;

            if (canActivate)
            {
                interactTxt.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    idolActivated?.Invoke();
                    fx.SetActive(true);
                    didInteract = true;
                }
            }
            else if(!canActivate)
            {
                if (didInteract)
                    fx.SetActive(true);
                else
                fx.SetActive(false);


                interactTxt.enabled = false;
            }
        }





        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawWireSphere(transform.position, radius);
        //}


    }

}