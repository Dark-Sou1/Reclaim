using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class PoisonEffect : DamageOverTImeEffect
    {
        public PoisonEffect(Targetable target, float damagePerSecond) : base(target, damagePerSecond) { }

        public override void UpdateEffect()
        {
            float dmg = damagePerSecond / GameManager.gameTickFrequency;

            if (target.currentHealth > dmg)
                target.Damage(dmg);
            else
                target.Damage(target.currentHealth - 1);
        }
    }
}