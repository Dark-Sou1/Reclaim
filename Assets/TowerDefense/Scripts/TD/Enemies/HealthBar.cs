using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Giacomo
{
    public class HealthBar : MonoBehaviour
    {
        public Targetable targetable;
        [SerializeField] protected Slider slider;

        private void Awake()
        {
            targetable = GetComponentInParent<Targetable>();
            targetable.HealthChanged += UpdateHealthBar;
        }

        protected void UpdateHealthBar()
        {
            slider.value = targetable.currentHealth / targetable.b_maxHealth;
        }
    }
}