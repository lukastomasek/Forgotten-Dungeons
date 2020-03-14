using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class EnemyAttackCollision : MonoBehaviour
	{
        [SerializeField] private GameObject attackPoint_1, attackPoint_2;

        public void ActivateAttackPoint1()
        {
            attackPoint_1.SetActive(true);
            StartCoroutine(AutomaticallyDeactivate());
        }

        public void ActivateAttackPoint2()
        {
            attackPoint_2.SetActive(true);
            StartCoroutine(AutomaticallyDeactivate());
        }

        public void DeactivateAttackPoint1()
        {
            if (attackPoint_1.activeInHierarchy)
                attackPoint_1.SetActive(false);
            else
                attackPoint_1.gameObject.SetActive(false);           
        }

        public void DeactivateAttackPoint2()
        {
            if (attackPoint_2.activeInHierarchy)
                attackPoint_2.SetActive(false);
            else
                attackPoint_2.gameObject.SetActive(false);          
        }

        private IEnumerator AutomaticallyDeactivate()
        {
            yield return new WaitForSeconds(.3f);
            attackPoint_1.gameObject.SetActive(false);
            attackPoint_2.gameObject.SetActive(false);
          
        }
	}

}