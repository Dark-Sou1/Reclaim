using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class SlowTower : AttackingTower 
    {
        public Stat SlowDuration;
        public Stat SlowAmount;

        AudioSource freezeSound;

        public override Stats GetStats()
        {
            if (stats != null)
                return stats;

            var tempStats = base.GetStats();
            tempStats.AddStat("slowDuration", SlowDuration);
            tempStats.AddStat("slowAmount", SlowAmount);
            return tempStats;
        }

        protected override void Attack()
        {
            foreach(Enemy e in GameManager.Enemies)
            {
                var dist = Vector2.Distance(transform.position, e.transform.position);
                if (dist < MinRange || dist > MaxRange) continue;

                e.Damage(Damage / AttackSpeed.BaseValue);


                if (e.stats.HasModifier("burn", "moveSpeed")) continue;
                StatModifierEffect slowEffect = new StatModifierEffect("freeze", e.stats);
                slowEffect.AddModifier("moveSpeed", multiply: SlowAmount);
                e.EffectHandler.AddEffect("freeze", slowEffect, SlowDuration);
            }
        }

        protected override void OnTargetFound()
        {
            if (freezeSound)
                Destroy(freezeSound.gameObject); 
            freezeSound = AudioController.Instance.PlaySound2D("tower_" + towerName + "_loop", attackSoundVolume, looping: true);

        }

        protected override void OnTargetLost()
        {
            freezeSound.Stop();
            Destroy(freezeSound.gameObject);
        }
        private void OnDisable()
        {
            if (freezeSound)
                Destroy(freezeSound.gameObject);
        }
    }

}