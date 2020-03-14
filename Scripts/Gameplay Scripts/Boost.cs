using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class Boost : MonoBehaviour
	{
        public delegate void OnDisabled();
        public static OnDisabled onDisabled;


        private void OnDisable()
        {
            onDisabled?.Invoke();
        }
    }

}