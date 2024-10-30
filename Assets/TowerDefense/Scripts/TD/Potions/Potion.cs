using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Giacomo
{
    public abstract class Potion : MonoBehaviour
    {
        public Sprite icon;
        public float cooldown;


        protected bool isSelected;
        protected float nextAvailableTime;
        public void Use() 
        {
            if(isSelected) 
            {
                isSelected = false;
                InputManager.Instance.SetPotionStatus(false);
                return;
            }

            if (!InputManager.Instance.acceptInput)
                return;
            if (nextAvailableTime > Time.time)
                return;

            isSelected = true;
            InputManager.Instance.SetPotionStatus(true);
        }

        private void Update()
        {
            if (!isSelected)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (Helpers.IsOverUI)
                {
                    Debug.Log("Clicked on UI");
                    return;
                }

                var mousePos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
                PlayPotion(mousePos);
                InputManager.Instance.SetPotionStatus(false);
                isSelected = false;
                nextAvailableTime = Time.time + cooldown;
            }
        }


        public abstract void PlayPotion(Vector2 position);
    }
}
