using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class WeaknessPotion : Potion
    {
        public float damageMultiplier;
        public float duration;

        public override void PlayPotion(Vector2 position)
        {
            foreach (Enemy e in GameManager.GetEnemies())
            {
                StatModifierEffect effect = new StatModifierEffect(e.stats);
                effect.AddModifier("damageTakenMultiplier", "weakness", multiply: damageMultiplier);

                e.EffectHandler.AddEffect("weakness", effect, duration);
            }     
        }
    }
}