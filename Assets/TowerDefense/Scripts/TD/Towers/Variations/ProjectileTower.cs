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

        [BoxGroup("Sound")]
        [Range(0f, 1f)]
        public float projectileHitSoundVolume = .5f;

        protected override void ManagedInitialize()
        {
            base.ManagedInitialize();
            stats.AddStat("projectileSpeed", b_projectileSpeed);
            stats.AddStat("splashDamageArea", b_splashDamageArea);
            stats.AddStat("projectileLifetime", b_projectileLifetime);
        }

        protected override void Attack()
        {
            GameObject go = Instantiate(projectilePrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
            string hitSFX = $"tower_{towerName}_hit";
            go.GetComponent<Projectile>().Initialize(stats["damage"], stats["projectileSpeed"], stats["projectileLifetime"], stats["splashDamageArea"], target, destroyProjectileOnTargetDeath, hitSFX, projectileHitSoundVolume);
        }
    }
}