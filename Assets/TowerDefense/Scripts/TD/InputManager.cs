using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class InputManager : Singleton<InputManager>
    {
        public bool acceptInput => !placingTower && !playingPotion && !movingTower;

        public bool playingPotion { get; protected set; }
        public bool placingTower { get; protected set; }
        public bool movingTower { get; protected set; }

        [SerializeField] GameObject cancelTooltip;


        public void SetPotionStatus(bool playing)
        {
            playingPotion = playing;
            cancelTooltip?.SetActive(!acceptInput);
        }

        public void SetPlacingStatus(bool placing)
        {
            placingTower = placing;
            cancelTooltip?.SetActive(!acceptInput);
        }

        public void SetMovingStatus(bool moving)
        {
            movingTower = moving;
            cancelTooltip?.SetActive(!acceptInput);
        }


    }

}