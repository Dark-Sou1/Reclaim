using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Giacomo
{
    [RequireComponent(typeof(ProgressBarUI))]
    public class HealthBar : MonoBehaviour
    {
        protected Targetable targetable;
        protected ProgressBarUI healthProgressBar;

        private void Awake()
        {
            targetable = GetComponentInParent<Targetable>();
            healthProgressBar = GetComponent<ProgressBarUI>();
            targetable.HealthChanged += UpdateHealthBar;
            healthProgressBar.ShowProgress(1, true);
        }

        protected void UpdateHealthBar()
        {
            float progress = targetable.currentHealth / targetable.stats["maxHealth"];
            healthProgressBar.ShowProgress(progress);
        }
    }
}