using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class StatModifierEffect : Effect
    {
        public Stats target;
        protected Dictionary<string, StatModifier> modifiers;

        public StatModifierEffect(Stats target)
        {
            modifiers = new Dictionary<string, StatModifier>();
            this.target = target;
        }

        public void AddModifier(string statName, string modifierName, float add = 0, float multiply = 1)
        {
            modifiers.Add(modifierName, new StatModifier(statName, add, multiply));
        }

        public override void StartEffect()
        {
            foreach (var mod in modifiers)
            {
                target.AddModifier(mod.Key, mod.Value.statName, mod.Value.add, mod.Value.multiply);
            }
        }


        public override void EndEffect()
        {
            foreach (var mod in modifiers)
            {
                target.RemoveModifier(mod.Key, mod.Value.statName);
            }
        }

        protected class StatModifier
        {
            public string statName;
            public float add;
            public float multiply;

            public StatModifier(string statName, float add, float multiply)
            {
                this.statName = statName;
                this.add = add;
                this.multiply = multiply;
            }
        }
    }
}