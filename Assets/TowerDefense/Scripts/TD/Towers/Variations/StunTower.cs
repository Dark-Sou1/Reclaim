using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Giacomo
{
    public class StunTower : AttackingTower 
    {

        [Header("Stun stats")]
        public float b_stunDuration = 1;
        public float b_stunChance = .5f;

        protected override void Initialize()
        {
            base.Initialize();
            stats.AddStat("stunDuration", b_stunDuration, 0);
            stats.AddStat("stunChance", b_stunChance, 0, 1);
        }

        protected override void Attack()
        {
            foreach (Enemy e in GameManager.GetEnemies())
            {
                var dist = Vector2.Distance(transform.position, e.transform.position);
                if (dist < stats["minRange"] || dist > stats["maxRange"]) continue;

                e.Damage(stats["damage"]);

                if (Random.Range(0, 1f) > stats["stunChance"]) continue;
                if (!e.TryGetComponent(out EffectHandler eh)) continue;

                StatModifierEffect stunEffect = new StatModifierEffect(e.stats);
                stunEffect.AddModifier("moveSpeed", "stun", multiply: 0);
                eh.AddEffect("stun", stunEffect, stats["stunDuration"]);
            }
        }
    }

}
