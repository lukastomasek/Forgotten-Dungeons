using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class Panel : MonoBehaviour
	{
	  
       public void Restart()
       {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
       }

        public void Resume()
        {
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

       public void BackToMM()
       {
         SceneManager.LoadSceneAsync("MainMenu");
       }
	}

}