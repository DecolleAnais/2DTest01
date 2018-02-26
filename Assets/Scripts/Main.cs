using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class Main : MonoBehaviour
    {

        public PizzaGame pizzaGame;

        // Use this for initialization
        void Start()
        {

            pizzaGame.Init();

            pizzaGame.PrepareActivity();

            pizzaGame.StartActivity();
        }

    }
}

