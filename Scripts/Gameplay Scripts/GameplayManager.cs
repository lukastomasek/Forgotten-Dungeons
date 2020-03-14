using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using LukasScripts.UI;
//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class GameplayManager : MonoBehaviour
	{
        [SerializeField] private Animator introAnim_1,introAnim_2;
        [SerializeField] private Camera animCamera_1,animCamera_2, realCamera;
        [SerializeField] private CanvasGroup buttom, top;
        [SerializeField] private float timer = 3f, actTimer = 5f;
        [SerializeField] private Text indicator_1, indicator_2, indicator_3;
        [SerializeField] private PlayerMovement player;
        [SerializeField] private GameObject portalFX;
        private bool secondAct = false;
        [SerializeField] private GameObject enemy;
        [SerializeField] private GameObject panel;
        [SerializeField] private Fader fader;
        public delegate void OnSecondAct();
        public static event OnSecondAct onSecondAct;

        private ParticleSystem[] fx;

        private int idolNum;
        private bool allIdolsActive = false;

        private void Awake()
        {
            if (fader == null)
                fader = FindObjectOfType<Fader>();
        }
        private void Start()
        {
            Time.timeScale = 1;
            StartCoroutine(IntroAnim());
            portalFX.SetActive(false);
           
        }

       
        private void OnEnable()
        {
            Idol.idolActivated += CheckActivatedIdols;
        }

        private void OnDisable()
        {
            Idol.idolActivated -= CheckActivatedIdols;    
        }

        public void ActivateResumePanel()
        {
            panel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        private void Update()
        {
          if(idolNum == 3)
          {
                allIdolsActive = true;
          }
          if (allIdolsActive)
          {
                portalFX.SetActive(true);
          }

            if (Input.GetKeyDown(KeyCode.Escape))
                ActivateResumePanel();
        
        }

        #region CutScenes
        private IEnumerator Act()
        {
            introAnim_2.Play(TagManager.Act);
            realCamera.gameObject.SetActive(false);
            animCamera_1.gameObject.SetActive(false);
            animCamera_2.gameObject.SetActive(true);
            buttom.alpha = 1;
            top.alpha = 1;
            player.enabled = false;
            enemy.gameObject.SetActive(true);
            NavMeshAgent nav = enemy.GetComponent<NavMeshAgent>();
            nav.isStopped = true;
            fx = enemy.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < fx.Length; i++)
            {
                fx[i].Play();
            }
            indicator_2.enabled = true;
            yield return new WaitForSeconds(actTimer);
            nav.isStopped = false;
            buttom.alpha = 0;
            top.alpha = 0;
            realCamera.gameObject.SetActive(true);
            indicator_2.enabled = false;
            if (animCamera_1.gameObject.activeInHierarchy)
                animCamera_1.gameObject.SetActive(false);

            animCamera_2.gameObject.SetActive(false);
            player.enabled = true;
            indicator_3.enabled = true;
            yield return new WaitForSeconds(2f);
            indicator_3.enabled = false;
           
        }

        private IEnumerator IntroAnim()
        {
            player.enabled = false;
            fader.FadeIn();
            buttom.alpha = 1f;
            top.alpha = 1f;
            introAnim_1.Play(TagManager.IntroAnim);
            indicator_1.enabled = true;
            if (!animCamera_1.gameObject.activeInHierarchy)
                animCamera_1.gameObject.SetActive(true);

            yield return new WaitForSeconds(timer);
            player.enabled = true;
            buttom.alpha = 0;
            top.alpha = 0;
            animCamera_1.gameObject.SetActive(false);
            realCamera.gameObject.SetActive(true);

            yield return new WaitForSeconds(1.5f);
            indicator_1.enabled = false;
        }

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.Player))
            {
                secondAct = true;
                onSecondAct?.Invoke();
                GetComponent<BoxCollider>().enabled = false;
            }

            if (secondAct)
            {
                StartCoroutine(Act());
            }
        }

        public bool SecondAct
        {
            get => secondAct;
        }

        private void CheckActivatedIdols()
        {
            idolNum++;
        }
    }

}