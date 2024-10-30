using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Giacomo
{
    public class Enemy : Targetable
    {
        public float b_moveSpeed = 1;
        public int b_damageToTower = 1;

        public FollowPathMovement movement;

        public EffectHandler EffectHandler;

        protected override void Initialize()
        {
            base.Initialize();

            if(!TryGetComponent(out EffectHandler))
                EffectHandler = gameObject.AddComponent<EffectHandler>();

            GameManager.AddEnemy(this);
            movement = GetComponent<FollowPathMovement>();
            movement.SetDestination(GridManager.Instance.GetHome());
            movement.OnArrive += ReachedHomeTile;

            stats.AddStat("moveSpeed", b_moveSpeed, 0);
            stats.AddStat("damageToTower", b_damageToTower, 0);
        }

        private void Update()
        {
            float moveAmount = stats["moveSpeed"] * Time.deltaTime;
            movement.Move(moveAmount);
        }

        protected void ReachedHomeTile()
        {
            GameStats.Instance?.LoseHP((int)stats["damageToTower"]);
            Kill();
        }

        protected override void Kill()
        {
            isAlive = false;
        }

        protected void Die()
        {
            GameManager.RemoveEnemy(this);
            Destroy(gameObject);
        }

        private void LateUpdate()
        {
            if (!isAlive)
            {
                Die();
            }
        }
    }
}