using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{

	public class SoundManager : MonoBehaviour
	{
        public static SoundManager instance;


        [SerializeField] private AudioClip[] footStepClips;
        [SerializeField] private AudioSource stepsAudio;

        private float curTime;

        [SerializeField]  private float walkDistance;
        [SerializeField]  private float runDistance;
        [SerializeField] private PlayerMovement player;
        [SerializeField] private float pitch = 1f;
        [SerializeField] private GameObject spottedScr;
        [SerializeField] private GameObject bgScr;
        [SerializeField] private GameObject chaseScr;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }
        private void Start()
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag(TagManager.Player).GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if(player.Controller.velocity.sqrMagnitude > 2f && player.Controller.velocity.sqrMagnitude < 12f)
            {
                stepsAudio.pitch = Random.Range(pitch - 0.5f, pitch + .5f);
                stepsAudio.volume = Random.Range(0.5f, 1f);
                stepsAudio.clip = footStepClips[1];
                stepsAudio.Play();
            }
            else if(player.Controller.velocity.sqrMagnitude > 12.1f)
            {
                stepsAudio.pitch = Random.Range(pitch - .5f, pitch + .5f);
                stepsAudio.volume = Random.Range(0.5f, 1f);
                stepsAudio.clip = footStepClips[Random.Range(0, footStepClips.Length)];
                stepsAudio.Play();
            }

           

        }


        public void PlayChaseMusic()
        {
            bgScr.SetActive(false);
            spottedScr.SetActive(true);
            StartCoroutine(PlayChaseSound());
        }


        private IEnumerator PlayChaseSound()
        {
            yield return new WaitForSeconds(1.5f);
            chaseScr.SetActive(true);
        }
    


	}

}