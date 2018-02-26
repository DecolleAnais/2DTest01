using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace MyGame
{
    public class PizzaGame : MonoBehaviour
    {
        public event Action OnPNJReadyToCook;

        [Header("UI Settings")]
        public PizzaGameUI pizzaGameUI;
        public string introMessage;
        public string listenMessage;
        public string readyMessage;
        public string verificationMessage;
        public string endMessage;

        [Header("Time Settings")]
        public float introTime = 2f;

        [Header("Sounds Settings")]
        public AudioSource audioSource;

        [Header("Config Settings")]
        public List<PizzaGameConfig> configs;
        private PizzaGameConfig currentConfig;

        [Header("PNJ Settings")]
        public GameObject PNJ;

        public float PNJ_Speed = 3f;

        public float PNJ_ZOffsetForPizza = 5f;

        public Transform PNJ_InitialPosition;
        public Transform PNJ_WaitingPosition;

        [Header("PJ Settings")]
        public GameObject PJ;

        public float PJ_Speed;

        public Transform PJ_InitialPosition;
        public Transform PJ_WaitingPosition;

        [Header("Pizzas Settings")]
        public int maxPizzasOnScreen = 4;
        public GameObject pizzaPrefab;
        public HorizontalLayoutGroup pizzasGroup;
        private List<GameObject> pizzasPool;
        
        private List<GameObject> currentPizzasGroup;
        private Transform currentPizzaPosition;
        private Vector3 nextPizzaPosition;


        public void Init()
        {
            pizzaGameUI.Init();

            pizzasPool = new List<GameObject>();

            for(int i = 0;i < maxPizzasOnScreen;i++)
            {
                GameObject pizza = (GameObject)Instantiate(pizzaPrefab);
                pizza.transform.SetParent(pizzasGroup.transform, false);
                pizza.SetActive(false);
                pizzasPool.Add(pizza);
            }
            
        }

        public void PrepareActivity()
        {
            PNJ.transform.position = new Vector3(PNJ_InitialPosition.position.x, PNJ.transform.position.y, PNJ_InitialPosition.position.z);

            //Debug.Log("[PizzaGame] PrepareActivity, PNJPosition : " + PNJ.transform.position);

            if (configs.Count > 0)
            {
                currentConfig = configs[0];

                currentPizzasGroup = new List<GameObject>();

                int i = 0;
                while (i < pizzasPool.Count && i < currentConfig.nbPizzas)
                {
                    if (!pizzasPool[i].activeInHierarchy)
                    {
                        pizzasPool[i].SetActive(true);

                        currentPizzasGroup.Add(pizzasPool[i]);
                    }
                    i++;
                }

                pizzasGroup.CalculateLayoutInputHorizontal();
                pizzasGroup.CalculateLayoutInputVertical();
                pizzasGroup.SetLayoutHorizontal();
                pizzasGroup.SetLayoutVertical();
                
            }
        }

        public void StartActivity()
        {

            if (currentPizzasGroup.Count > 0)
            {

                nextPizzaPosition = new Vector3(currentPizzasGroup[0].transform.position.x,
                                                PNJ.transform.position.y,
                                                currentPizzasGroup[0].transform.position.z + PNJ_ZOffsetForPizza);

                //Debug.Log("[PizzaGame] StartActivity, nextPizzaPosition : " + nextPizzaPosition);

                pizzaGameUI.SetMessage(introMessage);

                Invoke("PNJ_Start", introTime);                
            }
            
        }

        void PNJ_Start()
        {
            pizzaGameUI.SetMessage(String.Empty);

            StartCoroutine("PNJ_GoToTheNextPizza");

            OnPNJReadyToCook += PNJReadyToCook;
        }

        IEnumerator PNJ_GoToTheNextPizza()
        {
            while(PNJ.transform.position != nextPizzaPosition)
            {
                PNJ.transform.position = Vector3.MoveTowards(PNJ.transform.position, nextPizzaPosition, PNJ_Speed * Time.deltaTime);
                //Debug.Log("[PNJ_GoTOTheNextPizza] currentPNJPosition : " + PNJ.transform.position);

                yield return new WaitForEndOfFrame();
            }

            if (OnPNJReadyToCook != null)
                OnPNJReadyToCook();

            yield return null;
        }

        void PNJReadyToCook()
        {
            Debug.Log("[PizzaGame] PNJReadyToCook");

            StopCoroutine("PNJ_GoToTheNextPizza");
            OnPNJReadyToCook -= OnPNJReadyToCook;

        }

        void PNJ_OnTimerFinished()
        {

        }



        // Update is called once per frame
        void Update()
        {

        }


    }
}


