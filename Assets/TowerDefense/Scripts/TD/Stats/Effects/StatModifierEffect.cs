using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class StatModifierEffect : Effect
    {
        public Stats target;
        protected List<StatModifier> modifiers;
        protected string modifierName;

        public StatModifierEffect(string modifierName, Stats target)
        {
            modifiers = new();
            this.target = target;
            this.modifierName = modifierName;
        }

        public void AddModifier(string statName, float add = 0, float multiply = 1)
        {
            modifiers.Add(new StatModifier(statName, add, multiply));
        }

        public override void StartEffect()
        {
            foreach (var mod in modifiers)
            {
                target.AddModifier(modifierName, mod.statName, mod.add, mod.multiply);
            }
        }


        public override void EndEffect()
        {
            foreach (var mod in modifiers)
            {
                target.RemoveModifier(modifierName, mod.statName);
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