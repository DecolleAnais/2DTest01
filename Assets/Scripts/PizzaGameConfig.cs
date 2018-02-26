using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class PizzaGameConfig : MonoBehaviour
    {

        public int nbPizzas = 1;

        public int nbRepetitions = 1;

        public AudioClip timerSound;
        public AudioClip backgroundSound;
        public List<AudioClip> notesSounds;
    }

}

