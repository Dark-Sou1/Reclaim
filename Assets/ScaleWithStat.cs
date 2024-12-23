using Sirenix.Utilities;
using UnityEngine;

namespace Giacomo
{
    public class ScaleWithStat : MonoBehaviour
    {
        [SerializeField] string statName;
        [SerializeField] GameObject statObject;
        [SerializeField] float offset = 0;
        [SerializeField] float multiply = 1;
        Stat stat;

        void OnEnable()
        {
            if (statObject && !statName.IsNullOrWhitespace())
            {
                stat = statObject.GetComponent<Stats>().GetStat(statName);
                stat.OnValueChanged += UpdateScale;
            }
            UpdateScale(null);
        }

        public void SetStat(Stat stat)
        {
            if (this.stat != null)
                this.stat.OnValueChanged -= UpdateScale;

            this.stat = stat;
            this.stat.OnValueChanged += UpdateScale;
        }

        void UpdateScale(Stat.StatValueChangedEventArgs args)
        {
            if(stat == null)
            {
                transform.localScale = Vector3.zero;
                return;
            }

            transform.localScale = Vector3.one * (stat + offset) * multiply;
        }

        private void OnDisable()
        {
            if (stat != null)
                stat.OnValueChanged -= UpdateScale;
        }
    }
}