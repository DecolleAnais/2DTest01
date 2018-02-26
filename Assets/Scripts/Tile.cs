using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace MyGame
{
    public class Tile : MonoBehaviour, IPointerDownHandler,  IPointerUpHandler
    {

        public event Action<int> OnPress;
        public event Action<int> OnRelease;

        public GameObject tile;

        public Sprite imgOn;
        public Sprite imgOff;

        public bool isActive { get; private set; }

        private Image img;

        public void Init()
        {
            img = GetComponent<Image>();
            isActive = false;
        }

        public void Activate()
        {
            isActive = true;
        }

        public void Deactivate()
        {
            isActive = false;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData data)
        {
            if (isActive)
            {
                SetStateOn();

                if (OnPress != null)
                    OnPress(tile.gameObject.GetInstanceID());
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData data)
        {
            if (isActive)
            {
                SetStateOff();

                if (OnRelease != null)
                    OnRelease(tile.gameObject.GetInstanceID());
            }
        }

        public void SetStateOn()
        {
            img.overrideSprite = imgOn;
        }

        public void SetStateOff()
        {
            img.overrideSprite = imgOff;
        }
    }

}

