using System;
using UnityEngine;

public class CardStat : MonoBehaviour
{
   [NonSerialized] public Color color;

   private void Start()
   {
      color = GetComponent<SpriteRenderer>().color;
   }
}
