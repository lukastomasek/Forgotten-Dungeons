using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{

	public class Rotate : MonoBehaviour
	{
        private float speed = 1f;
        private float angle;


        private void Update()
        {
            angle = (angle + speed) % 360f;
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

}