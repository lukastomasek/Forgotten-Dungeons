using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class PlayerHealth : MonoBehaviour
	{
        private int currentHealth = 100;
        private int maxHealth = 100;
        private bool playerDied = false;

        [SerializeField] private Text healthUI;
        private GameplayManager gameplayManager;

        private void Start()
        {
            currentHealth = maxHealth;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthUI.text = currentHealth.ToString();
            gameplayManager = FindObjectOfType<GameplayManager>();

        }


        private void Update()
        {
            if (playerDied)
            {
                PlayerDied();
            }
        }

        public void ApplyDamage(int damage)
        {
            currentHealth -= damage;
            healthUI.text = currentHealth.ToString();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                playerDied = true;
               
            }

            
        }

        private void PlayerDied()
        {
            gameplayManager.ActivateResumePanel();
        }


        public bool PlayerIsdead
        {
            get => playerDied;
        }
    }

}