using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukasScripts.UI;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class PlayerMovement : MonoBehaviour
	{
        #region values and references
        private float x, z;
        private Vector3 moveDir;
        private CharacterController controller;
        private Vector3 velocity;
        private bool isGrounded;
        private bool isSpriniting;
        private float currentStamina;
        private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
        private Coroutine regen;

        [SerializeField]
        private float walkSpeed = 12f;

        [SerializeField]
        private float jumpHeight = 3f;

        [SerializeField]
        private float gravity = -9.81f;

        [SerializeField]
        private Transform groundCheck;

        [SerializeField]
        private float groundDistance = .4f;

        [SerializeField]
        private LayerMask groundMask;

        [SerializeField]
        private float sprintSpeed = 18f;

        [SerializeField]
        private float maxStamina = 100f;

        [SerializeField]
        private float amount = 10f;

        [SerializeField]
        private float treshold = 50f;

        [SerializeField]
        [Range(0.2f,5f)]
        private float walkDistance, runDistance;

        [SerializeField]
        private StaminaBar staminaBar;

        [SerializeField]
        private SoundManager soundManager;

        

        #endregion

        private void Awake()
        {
            controller = GetComponent<CharacterController>();

            if(staminaBar == null)
                staminaBar = FindObjectOfType<StaminaBar>();

            if (soundManager == null)
                soundManager = FindObjectOfType<SoundManager>();

        
        }

        private void Start()
        {
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

            currentStamina = maxStamina;

            staminaBar.SetMaxStamina(maxStamina);
        }

        private void Update()
        {
            Move(walkSpeed);
            Sprint();      
        }

        private void Move(float value)
        {
            isGrounded =
              Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            // direction that player is facing
            moveDir = transform.right * x + transform.forward * z;
            controller.Move(moveDir * value * Time.deltaTime);
           

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }


        private void Sprint()
        { 
            if (currentStamina == maxStamina)
                isSpriniting = true;

            if (isSpriniting && SprintBtn() == true && z != 0)
            {
               
                currentStamina -= amount;
                staminaBar.SetStamina(currentStamina);
                Move(sprintSpeed);

                if (regen != null)
                    StopCoroutine(regen);

                regen = StartCoroutine(RechargeStamina(1.1f));
            }
            
            if(currentStamina <= 0)
            {
                currentStamina = 0;
                isSpriniting = false;          
            }        
        }

        private IEnumerator RechargeStamina(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            while(currentStamina < maxStamina)
            {
                currentStamina += maxStamina / 100f;
                staminaBar.SetStamina(currentStamina);
                yield return regenTick;
            }

            if (currentStamina != maxStamina)
                currentStamina = maxStamina;

            isSpriniting = true;

            regen = null;
        }


        private bool SprintBtn()
        {
            return Input.GetButton("Fire3");
        }

        public CharacterController Controller
        {
            get => controller;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Boost))
            {
                if (currentStamina == maxStamina)
                    return;

                currentStamina = maxStamina;
                staminaBar.SetStamina(currentStamina);
                other.gameObject.SetActive(false);
            }
        }
    }

}