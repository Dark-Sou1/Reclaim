using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Giacomo 
{
    public class DamageOverTImeEffect : Effect
    {
        public Targetable target;
        public float damagePerSecond;

        public DamageOverTImeEffect(Targetable target, float damagePerSecond)
        {
            this.target = target;
            this.damagePerSecond = damagePerSecond;
        }

        public override void UpdateEffect()
        {
            target.Damage(damagePerSecond / GameManager.gameTickFrequency);
        }

    }
}
