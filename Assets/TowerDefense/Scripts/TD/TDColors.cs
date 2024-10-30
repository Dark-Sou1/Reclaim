using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class TDColors : Singleton<TDColors>
    {
        public Color affordableColor;
        public Color unaffordableColor;
        public static Color AffordableColor => TDColors.Instance.affordableColor;
        public static Color UnaffordableColor => TDColors.Instance.unaffordableColor;


    }
}