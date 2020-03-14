using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class MouseLook : MonoBehaviour
	{
        private float mouseX, mouseY;

        [SerializeField]
        private Transform playerBody;
        private float xRotation = 0f;

        [SerializeField]
        private float mouseSensitivity = 100f;



        private void Start()
        {
           // playerBody = GetComponentInParent<Transform>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

    }

}