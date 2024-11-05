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
        [DisableInEditorMode]
        public float currentHealth = 0;
        public float b_maxHealth;
        public bool isAlive = true;

        public event Action HealthChanged;

        public Stats stats;

        public virtual void Start()
        {
            Initialize();
        }

        protected bool isInitialized;
        public virtual void Initialize()
        {
            if(isInitialized) return;
            isInitialized = true;

            stats = gameObject.AddComponent<Stats>();
            stats.AddStat("maxHealth", b_maxHealth);
            stats.AddStat("damageTakenMultiplier", 1);
            currentHealth = stats["maxHealth"];
            stats["maxHealth"].OnValueChanged += OnMaxHealthChanged;
        }

        protected void OnMaxHealthChanged(Stat.StatValueChangedEventArgs args)
        {
            currentHealth = currentHealth / args.previousValue * args.newValue;
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