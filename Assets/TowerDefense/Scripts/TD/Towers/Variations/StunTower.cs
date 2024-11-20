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

        public Transform impactEffect;

        protected override void ManagedInitialize()
        {
            base.ManagedInitialize();
            stats.AddStat("stunDuration", b_stunDuration, 0);
            stats.AddStat("stunChance", b_stunChance, 0, 1);
        }

        protected override void Attack()
        {
            foreach (Enemy e in GameManager.Enemies)
            {
                var dist = Vector2.Distance(transform.position, e.transform.position);
                if (dist < stats["minRange"] || dist > stats["maxRange"]) continue;

                e.Damage(stats["damage"]);

                if (Random.Range(0, 1f) > stats["stunChance"]) continue;

                StatModifierEffect stunEffect = new StatModifierEffect(e.stats);
                stunEffect.AddModifier("moveSpeed", "stun", multiply: 0);
                e.EffectHandler.AddEffect("stun", stunEffect, stats["stunDuration"]);
            }
            StartCoroutine(AttackEffect());
        }

        //does not work with high fire rates. to fix I'd need to increase the animation speed (but this is fine for now)
        protected IEnumerator AttackEffect()
        {
            impactEffect.gameObject.SetActive(true);
            yield return Helpers.GetWait(0.2f);
            impactEffect.gameObject.SetActive(false);
        }
    }
}
