using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace Giacomo
{
    public class Stats : MonoBehaviour
    {
        [ShowInInspector]
        public Dictionary<string, Stat> stats {  get; private set; }

        public Stat this[string s] => stats[s];
        public Stats()
        {
            stats = new Dictionary<string, Stat>();
        }

        public void AddStat(string name, float baseValue, float minValue = float.MinValue, float maxValue = float.MaxValue)
        {
            if (stats.ContainsKey(name))
                Debug.LogError("duplicate stat: " + name + ": " + baseValue);
            stats.Add(name, new Stat(baseValue, minValue, maxValue));
        }

        public void AddModifier(string modifierName, string statName, float add = 0, float multiply = 1, bool keepHighestIfDuplicate = true)
            => stats[statName].AddModifier(modifierName, add, multiply, keepHighestIfDuplicate);

        public bool HasModifier(string modifierName, string statName)
            => stats[statName].HasModifier(modifierName);

        public void RemoveModifier(string modifierName, string statName)
            => stats[statName].RemoveModifier(modifierName);

        public Stat GetStat(string name) => stats[name];
    }

    [Serializable]
    public class Stat
    {
        [ShowInInspector]
        public float baseValue { get; private set; }
        [ShowInInspector, ReadOnly]
        public float value { get; private set; }
        [ShowInInspector]
        public float minValue { get; private set; }
        [ShowInInspector]
        public float maxValue { get; private set; }
        [ShowInInspector]
        public Dictionary<string, StatModifier> modifiers { get; private set; }

        public event Action<StatValueChangedEventArgs> OnValueChanged;

        public static implicit operator float(Stat s) => s.value;

        public Stat()
        {
            modifiers = new Dictionary<string, StatModifier>();
        }
        public Stat(float baseValue, float minValue, float maxValue) : base()
        {
            this.baseValue = baseValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
            modifiers = new Dictionary<string, StatModifier>();
            UpdateValue();
        }

        public void SetBaseValue(float newValue)
        {
            baseValue = newValue;
            UpdateValue();
        }

        public void AddModifier(string id, float add = 0, float multiply = 1, bool keepHighestIfDuplicate = true)
        {
            if (modifiers.ContainsKey(id))
            {
                if (!keepHighestIfDuplicate)
                {
                    modifiers[id].multiply = Mathf.Max(modifiers[id].multiply, multiply);
                    modifiers[id].add = Mathf.Max(modifiers[id].add, add);
                }
                else
                {
                    modifiers[id].multiply = Mathf.Min(modifiers[id].multiply, multiply);
                    modifiers[id].add = Mathf.Min(modifiers[id].add, add);
                }
            }
            else
            {
                modifiers[id] = new StatModifier(add, multiply);
            }
            UpdateValue();
        }

        public bool HasModifier(string id)
        {
            return modifiers.ContainsKey(id);
        }
        public void RemoveModifier(string id)
        {
            if (modifiers.ContainsKey(id))
                modifiers.Remove(id);
            UpdateValue();
        }

        protected void UpdateValue()
        {
            float add = modifiers.Sum(x => x.Value.add);
            float multiply = 1 + modifiers.Sum(x => x.Value.multiply - 1);

            float res = baseValue * multiply + add;
            float newVal = Mathf.Clamp(res, minValue, maxValue);

            float previousValue = value;
            value = newVal;

            if (previousValue != newVal)
                OnValueChanged?.Invoke(new StatValueChangedEventArgs(previousValue, newVal));
        }

        public class StatValueChangedEventArgs
        {
            public float previousValue;
            public float newValue;
            public StatValueChangedEventArgs(float previousValue, float newValue)
            {
                this.previousValue = previousValue;
                this.newValue = newValue;
            }
        }

        [Serializable]
        public class StatModifier
        {
            public float add;
            public float multiply;
            public StatModifier() { }
            public StatModifier(float add, float multiply)
            {
                this.add = add;
                this.multiply = multiply;
            }
        }
    }
}