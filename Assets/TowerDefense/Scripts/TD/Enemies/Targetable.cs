using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Giacomo
{
    public class Targetable : MonoBehaviour
    {
        [DisableInEditorMode, PropertyRange(0, "b_maxHealth")]
        public float currentHealth;
        public float b_maxHealth;
        public bool isAlive = true;

        public event Action HealthChanged;

        public Stats stats;

        public virtual void Start()
        {
            stats = gameObject.AddComponent<Stats>();
            Initialize();
        }

        protected virtual void Initialize()
        {
            stats.AddStat("maxHealth", b_maxHealth);
            stats.AddStat("damageTakenMultiplier", 1);
            currentHealth = stats["maxHealth"];
        }

        public void Damage(float amount)
        {
            if (amount <= 0)
                return;

            currentHealth -= amount * stats["damageTakenMultiplier"];

            HealthChanged?.Invoke();

            if (currentHealth <= 0)
                isAlive = false;
        }

        public void Heal(float amount)
        {
            if (amount <= 0)
                return;

            currentHealth += amount;

            HealthChanged?.Invoke();

            if (currentHealth > stats["maxHealth"])
                currentHealth = stats["maxHealth"];
        }

        protected virtual void Kill()
        {
            Destroy(gameObject);
        }
    }
}