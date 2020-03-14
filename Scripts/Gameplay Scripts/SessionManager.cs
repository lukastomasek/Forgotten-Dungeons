using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class SessionManager : MonoBehaviour
	{
        [SerializeField] private Button playBtn, quitBtn, creditsBtn, exitCreditBtn, exitHowToBtn, howToPlayBtn;
        [SerializeField] private Text loadingTxt;
        [SerializeField] private GameObject creditsPanel, howToPanel;
        [SerializeField] private float loadTimer = 2f;


        private void Start()
        {
            if (loadingTxt.enabled)
                loadingTxt.enabled = false;

            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            playBtn.onClick.AddListener(() => LoadLevelManually(loadTimer));
            quitBtn.onClick.AddListener(() => QuitGame());
            creditsBtn.onClick.AddListener(() => ShowCredits());
            exitCreditBtn.onClick.AddListener(() => CloseCredits());
            howToPlayBtn.onClick.AddListener(() => ShowHowToPanel());
            exitHowToBtn.onClick.AddListener(() => CloseHowToPanel());
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private void ShowCredits()
        {
            creditsPanel.SetActive(true);
        }
        private void ShowHowToPanel() => howToPanel.SetActive(true);

        private void CloseHowToPanel() => howToPanel.SetActive(false);

        private void CloseCredits()
        {
            creditsPanel.SetActive(false);
        }

        private void LoadLevel()
        {
            StartCoroutine(LoadSceneASync());
        }

        private IEnumerator LoadSceneASync()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(TagManager.level1, LoadSceneMode.Single);

            while (!op.isDone)
            {
                loadingTxt.enabled = true;

                yield return null;
            }
        }


        private void LoadLevelManually(float t) => StartCoroutine(LoadLevelM(t));

        private IEnumerator LoadLevelM(float t)
        {
            loadingTxt.enabled = true;
            yield return new WaitForSeconds(t);
            SceneManager.LoadSceneAsync(TagManager.level1, LoadSceneMode.Single); 
        }


	}

}