using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Giacomo
{
    public class MinigameInfoUI : Singleton<MinigameInfoUI>
    {
        [SerializeField] GameObject parent;

        [SerializeField] Image iconImage;
        [SerializeField] TMP_Text titleText;
        [SerializeField] TMP_Text descriptionText;

        private void Awake()
        {
            Hide(null);
        }

        object shownBy;
        public void Show(object shownBy, Sprite icon, string title, string description)
        {
            parent.SetActive(true);
            iconImage.sprite = icon;
            titleText.text = title;
            descriptionText.text = description;
            this.shownBy = shownBy;
        }

        public void Hide(object hideInfoShownBy)
        {
            if (shownBy != hideInfoShownBy)
                return;

            ForceHide();
        }
        public void ForceHide()
        {
            parent.SetActive(false);
        }
    }
}