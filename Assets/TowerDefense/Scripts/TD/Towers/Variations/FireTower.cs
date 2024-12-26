using UnityEngine;

namespace Giacomo
{
    public class FireTower : AttackingTower
    {
        [Header("Fire Attack")]
        public ParticleSystem fireParticles;
        public Collider2D attackCollider;

        public Stat BurnDuration;
        public Stat BurnDPS;

        AudioSource fireSound;
        RaycastHit2D[] hit = new RaycastHit2D[50];
        ContactFilter2D contactFilter = new ContactFilter2D();

        protected override void ManagedInitialize()
        {
            base.ManagedInitialize();
            contactFilter.useTriggers = true;
            fireParticles.Stop();
        }

        public override Stats GetStats()
        {
            if (stats != null)
                return stats;

            var tempStats = base.GetStats();
            tempStats.AddStat("burnDuration", BurnDuration);
            tempStats.AddStat("burnDPS", BurnDPS);
            return tempStats;
        }

        protected override void Attack()
        {
            int hitCount = attackCollider.Cast(Vector2.zero, contactFilter, hit);
            for (int i = 0; i < hitCount; i++) 
            {
                if (hit[i].collider == null) continue;
                if (!hit[i].collider.TryGetComponent(out Targetable t)) continue;
                if (!hit[i].collider.TryGetComponent(out EffectHandler e)) continue;

                t.Damage(Damage / AttackSpeed.BaseValue);

                DamageOverTImeEffect burnEffect = new DamageOverTImeEffect(t, BurnDPS);
                e.AddEffect("burn", burnEffect, BurnDuration);
                e.RemoveEffect("freeze");
            }
        }

        protected override void OnTargetFound()
        {
            fireParticles.Play();
            if (fireSound)
                Destroy(fireSound.gameObject);
            fireSound = AudioController.Instance.PlaySound2D("tower_" + towerName + "_loop", attackSoundVolume, looping: true);
        }

        protected override void OnTargetLost()
        {
            fireParticles.Stop();
            Destroy(fireSound.gameObject);
        }
        private void OnDisable()
        {
            if(fireSound)
                Destroy(fireSound.gameObject);
        }
    }
}