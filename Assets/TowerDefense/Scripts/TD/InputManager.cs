using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool acceptInput => !placingTower && !placingTower;

    public bool playingPotion { get; protected set; }
    public bool placingTower { get; protected set; }

    public void SetPotionStatus(bool playing)
    {
        playingPotion = playing;
    }

    public void SetPlacingStatus(bool placing)
    {
        placingTower = placing;
    }
}
