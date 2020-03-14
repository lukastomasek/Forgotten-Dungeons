using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LukasScripts.UI;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{

	public class EndMission : MonoBehaviour
	{
        private float timer = 2f;
        public GameObject youWonTxt;
        public Image bg;
        int alpha;
        float time = -1;
        [SerializeField] private Fader fader;

        private void Start()
        {
            if (fader == null)
                fader = FindObjectOfType<Fader>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                StartCoroutine(Wait());
            }
        }

        IEnumerator Wait()
        {
            youWonTxt.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            fader.FadeOut();
            yield return new WaitForSeconds(timer);
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

}