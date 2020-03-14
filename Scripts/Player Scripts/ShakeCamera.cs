using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{

	public class ShakeCamera : MonoBehaviour
	{
        [SerializeField] private float power = 0.2f;
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private float slowDownTime = 1f;

        private bool canShake;
        private float initDuration;
        private Vector3 startPosition;


        private void Start()
        {
            startPosition = transform.position;
            initDuration = duration;
        }

        private void Update()
        {
            if (canShake)
            {
                if(duration > 0f)
                {
                    Vector3 temp = transform.position;

                    transform.localPosition = startPosition +
                        Random.insideUnitSphere * power;
                    
                    duration -= Time.deltaTime * slowDownTime;
                }
                else
                {
                    canShake = false;
                    duration = initDuration;
                    transform.localPosition = startPosition;

                    return;
                }
            }
        }


        public bool CanShakeCamera
        {
            get => canShake;
            set => canShake = value;
        }

    }

}