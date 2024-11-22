using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Giacomo
{
    public abstract class Potion : MonoBehaviour
    {
        //public Sprite icon;
        public float cooldown;
        public float range = 10;

        [SerializeField] GameObject previewCursor;

        public float NextAvailableTime => nextAvailableTime;
        protected float nextAvailableTime;
        protected bool isSelected;

        public event Action OnUsed;

        public void Use() 
        {
            //CANCEL
            if(isSelected) 
            {
                CancelPlaying();
                return;
            }

            if (!InputManager.Instance.acceptInput)
                return;
            if (nextAvailableTime > Time.time)
                return;

            //SELECT
            isSelected = true;
            InputManager.Instance.SetPotionStatus(true);
            previewCursor.SetActive(true);
        }

        private void Update()
        {
            //pause cooldown when spawning is paused
            if (!WaveManager.Instance.SpawningPaused 
                && GameManager.Instance.enemies.Count == 0)
                nextAvailableTime += Time.deltaTime;

            if (!isSelected)
                return;

            //CANCEL
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelPlaying();
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (Helpers.IsOverUI)
                {
                    Debug.Log("Clicked on UI");
                    return;
                }


                //PLAY
                var mousePos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
                
                PlayPotion(mousePos);
                
                isSelected = false;
                previewCursor.SetActive(false);
                InputManager.Instance.SetPotionStatus(false);
                nextAvailableTime = Time.time + cooldown;
                OnUsed?.Invoke();
            }
        }

        protected void CancelPlaying()
        {
            isSelected = false;
            previewCursor.SetActive(false);
            InputManager.Instance.SetPotionStatus(false);
        }


        public abstract void PlayPotion(Vector2 position);
    }
}
