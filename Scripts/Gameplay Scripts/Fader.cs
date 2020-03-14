using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts.UI{



	public class Fader : MonoBehaviour
	{
        [SerializeField] private Image img;
        [SerializeField] private float speed = 2f;
        private bool isFadeIn;
        private float t;
        private Color c = Color.black;



        public void FadeIn()
        {
            isFadeIn = true;
            StartCoroutine("Fade");
        }


        public void FadeOut()
        {
            isFadeIn = false;
            StartCoroutine("Fade");
        }



        private IEnumerator Fade()
        {
            t = 0;

            while (t < 1)
            {
                t += Time.deltaTime * speed;
                img.enabled = true;
                if (isFadeIn)
                    c.a = Mathf.Lerp(1, 0, t);
                else
                    c.a = Mathf.Lerp(0, 1, t);

                img.color = c;

                yield return null;
            }

            if (isFadeIn)
                img.enabled = false;
        }
	}





}