using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts.UI{

	public class StaminaBar : MonoBehaviour
	{
        private Slider slider;

        [SerializeField] private Gradient gradient;
         private Image fillImg;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            fillImg = GetComponentInChildren<Image>();
        }


        public void SetMaxStamina(float maxStamina)
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;


            // we can evaluate the color based on number
            // 1 is in the right  of the graidient
            fillImg.color = gradient.Evaluate(1f);
           
        }

        public void SetStamina(float stamina)
        {
            slider.value = stamina;

            // we use normaloized value which is 0 to 1 to give us
            // proper value for gradient colors
            fillImg.color = gradient.Evaluate(slider.normalizedValue);
        }

    }

}