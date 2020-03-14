using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DEVELOPER : LUKAS TOMASEK

//ALL RIGHTS RESERVED


namespace LukasScripts{



	public class BoostManager : MonoBehaviour
	{
        [SerializeField] private Transform[] points;

        [SerializeField] private GameObject staminaBoostPrefab;

        [SerializeField] private int maxBoosts = 5;
      

        private List<GameObject> boostList = new List<GameObject>();

        private int random;
        private bool allBoostDisabled = false;
        private GameObject go;
        public int disabledCount;

        private void Start()
        {
            disabledCount = maxBoosts;
                   
        }

      
        private void SpawnBoosts()
        {
            for (int i = 0; i < maxBoosts; i++)
            {
                random = Random.Range(0, points.Length);
                go = Instantiate(staminaBoostPrefab, points[random].position, Quaternion.identity);
                go.transform.parent = transform;
                boostList.Add(go);
            }
        }

        private void OnEnable()
        {
            Boost.onDisabled += Check;
            GameplayManager.onSecondAct += SpawnBoosts;
        }

        private void OnDisable()
        {
            Boost.onDisabled -= Check;
            GameplayManager.onSecondAct -= SpawnBoosts;
        }

        private void Update()
        {
          if(disabledCount == 0)
          {
             allBoostDisabled = true;
          }

            if (allBoostDisabled)
            {
                foreach (var boost in boostList)
                {
                    int newRand = Random.Range(0, points.Length);
                    boost.transform.position = points[newRand].position;
                    boost.SetActive(true);
                    allBoostDisabled = false;
                    disabledCount = maxBoosts;
                }
            }
               
        }

             
        private void Check()
        {
            disabledCount--;     
        }
     
	
	}

}