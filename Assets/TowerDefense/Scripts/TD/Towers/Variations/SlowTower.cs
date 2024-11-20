using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class SlowTower : AttackingTower 
    {
        [Header("Slow stats")]
        public float b_slowDuration = 2;
        public float b_slowAmount = .7f;

        protected override void ManagedInitialize()
        {
            base.ManagedInitialize();
            stats.AddStat("slowDuration", b_slowDuration, 0);
            stats.AddStat("slowAmount", b_slowAmount, 0, 1);
        }

        protected override void Attack()
        {
            foreach(Enemy e in GameManager.Enemies)
            {
                var dist = Vector2.Distance(transform.position, e.transform.position);
                if (dist < stats["minRange"] || dist > stats["maxRange"]) continue;

                e.Damage(stats["damage"] / stats["attackSpeed"].baseValue);


                if (e.stats.HasModifier("burn", "moveSpeed")) continue;
                StatModifierEffect slowEffect = new StatModifierEffect(e.stats);
                slowEffect.AddModifier("moveSpeed", "freeze", multiply: stats["slowAmount"]);
                e.EffectHandler.AddEffect("freeze", slowEffect, stats["slowDuration"]);
            }
        }
    }

}