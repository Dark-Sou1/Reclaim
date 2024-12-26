using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Giacomo
{
    public class ProjectileTower : AttackingTower
    {
        [Header("Projectile")]
        public GameObject projectilePrefab;
        public Transform bulletSpawnpoint;
        public bool destroyProjectileOnTargetDeath = true;
        public float b_projectileSpeed = 5;
        public float b_splashDamageArea = 0;
        public float b_projectileLifetime = 10;

        public Stat ProjectileSpeed;
        public Stat SplashDamageArea;
        public Stat ProjectileLifetime;

        [BoxGroup("Sound")]
        [Range(0f, 1f)]
        public float projectileHitSoundVolume = .5f;

        public override Stats GetStats()
        {
            if (stats != null)
                return stats;

            var tempStats = base.GetStats();
            tempStats.AddStat("projectileSpeed", ProjectileSpeed);
            tempStats.AddStat("SplashDamageArea", SplashDamageArea);
            tempStats.AddStat("ProjectileLifetime", ProjectileLifetime);
            return tempStats;
        }

        protected override void Attack()
        {
            GameObject go = Instantiate(projectilePrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
            string hitSFX = $"tower_{towerName}_hit";
            go.GetComponent<Projectile>().Initialize(Damage, ProjectileSpeed, ProjectileLifetime, SplashDamageArea, target, destroyProjectileOnTargetDeath, hitSFX, projectileHitSoundVolume);
        }
    }
}