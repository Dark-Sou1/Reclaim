using System;
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

        [SerializeField] GameObject previewCursor;

        public float NextAvailableTime => nextAvailableTime;
        protected float nextAvailableTime;
        protected bool isSelected;

        public event Action OnUsed;

        public void Use() 
        {
            if(isSelected) 
            {
                isSelected = false;
                InputManager.Instance.SetPotionStatus(false);
                previewCursor.SetActive(false);
                return;
            }

            if (!InputManager.Instance.acceptInput)
                return;
            if (nextAvailableTime > Time.time)
                return;

            isSelected = true;
            InputManager.Instance.SetPotionStatus(true);
            previewCursor.SetActive(true);
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
                
                isSelected = false;
                previewCursor.SetActive(false);
                InputManager.Instance.SetPotionStatus(false);
                nextAvailableTime = Time.time + cooldown;
                OnUsed?.Invoke();
            }
        }


        public abstract void PlayPotion(Vector2 position);
    }
}
