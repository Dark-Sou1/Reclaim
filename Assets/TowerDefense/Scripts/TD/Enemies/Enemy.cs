using UnityEngine;

namespace Giacomo
{
    public class Enemy : Targetable
    {
        public float b_moveSpeed = 1;
        public int b_damageToTower = 1;
        public int moneyReward = 10;

        public FollowPathMovement movement;

        public EffectHandler EffectHandler;

        public override void Initialize()
        {
            if (isInitialized) return;
            base.Initialize();

            if(!TryGetComponent(out EffectHandler))
                EffectHandler = gameObject.AddComponent<EffectHandler>();
            
            stats.AddStat("moveSpeed", b_moveSpeed, 0);
            stats.AddStat("damageToTower", b_damageToTower, 0);

            GameManager.AddEnemy(this);

            movement = GetComponent<FollowPathMovement>();
            movement.SetDestination(GridManager.Instance.GetHome());
            movement.OnArrive += ReachedHomeTile;
            if (movement.path?.Count <= 1)
                Debug.LogError($"Path not found ({name})", gameObject);

            isInitialized = true;
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
            GameStats.Instance.ModifyCoins(moneyReward);

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