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
            foreach (Enemy e in GameManager.Enemies)
            {
                if (Vector2.Distance(e.transform.position, position) > range)
                    continue;

                StatModifierEffect effect = new StatModifierEffect("weakness", e.stats);
                effect.AddModifier("damageTakenMultiplier", multiply: damageMultiplier);

                e.EffectHandler.AddEffect("weakness", effect, duration);
            }     
        }
    }
}