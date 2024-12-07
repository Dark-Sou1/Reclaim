using UnityEngine;

namespace Giacomo
{
    public class FireTower : AttackingTower
    {
        [Header("Fire Attack")]
        public float b_burnDuration = 2;
        public float b_burnDPS = 5;
        public ParticleSystem fireParticles;
        public Collider2D attackCollider;

        AudioSource fireSound;
        RaycastHit2D[] hit = new RaycastHit2D[50];
        ContactFilter2D contactFilter = new ContactFilter2D();

        protected override void ManagedInitialize()
        {
            base.ManagedInitialize();
            contactFilter.useTriggers = true;
            stats.AddStat("burnDuration", b_burnDuration);
            stats.AddStat("burnDPS", b_burnDPS);
            fireParticles.Stop();
        }

        protected override void Attack()
        {
            int hitCount = attackCollider.Cast(Vector2.zero, contactFilter, hit);
            for (int i = 0; i < hitCount; i++) 
            {
                if (hit[i].collider == null) continue;
                if (!hit[i].collider.TryGetComponent(out Targetable t)) continue;
                if (!hit[i].collider.TryGetComponent(out EffectHandler e)) continue;

                t.Damage(stats["damage"] / stats["attackSpeed"].baseValue);

                DamageOverTImeEffect burnEffect = new DamageOverTImeEffect(t, stats["burnDPS"]);
                e.AddEffect("burn", burnEffect, stats["burnDuration"]);
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