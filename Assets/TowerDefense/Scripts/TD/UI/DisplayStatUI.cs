using TMPro;
using UnityEngine;

namespace Giacomo
{
    public class DisplayStatUI : MonoBehaviour
    {
        public TMP_Text statName;
        public TMP_Text statValue;

        public void Show(string name, float value)
        {
            statName.text = name;
            statValue.text = value.ToString();
        }

    }
}