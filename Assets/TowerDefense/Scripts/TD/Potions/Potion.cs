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
        public string potionName;
        [TextArea]
        public string potionDescription;

        public float cooldown;
        public float range = 10;

        [SerializeField] GameObject previewCursor;
        [SerializeField] GameObject potionVFX;

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
            if (!WaveManager.Instance.isSpawningEnemies 
                && GameManager.Instance.enemies.Count == 0
                && WaveManager.Instance.SpawningPaused)
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
                if (potionVFX)
                {
                    potionVFX.transform.position = mousePos;
                    potionVFX.SetActive(true);
                    potionVFX.transform.localScale = Vector3.one * range / 2;
                }

                isSelected = false;
                previewCursor.SetActive(false);
                InputManager.Instance.SetPotionStatus(false);
                nextAvailableTime = Time.time + cooldown;
                OnUsed?.Invoke();
            }
        }

        public void OnCursorHover()
        {
            DisplayInfoUI.Instance.Show(this, icon, potionName, potionDescription);
        }
        public void OnCursorExit()
        {
            DisplayInfoUI.Instance.Hide(this);
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
