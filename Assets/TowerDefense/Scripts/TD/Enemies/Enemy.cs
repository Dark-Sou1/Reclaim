using UnityEngine;

namespace Giacomo
{
    public class Enemy : Targetable
    {
        public float b_moveSpeed = 1;
        public float maxPositionOffset = 0.25f;
        public int b_damageToTower = 1;
        public int moneyReward = 10;

        public FollowPathMovement movement;

        [HideInInspector]
        public EffectHandler EffectHandler;

        protected override void ManagedInitialize()
        {
            base.ManagedInitialize();

            if(!TryGetComponent(out EffectHandler))
                EffectHandler = gameObject.AddComponent<EffectHandler>();
            
            stats.AddStat("moveSpeed", b_moveSpeed, 0);
            stats.AddStat("damageToTower", b_damageToTower, 0);

            movement = GetComponent<FollowPathMovement>();
            Vector2 positionOffset = new Vector2(
                Random.Range(-maxPositionOffset, maxPositionOffset),
                Random.Range(-maxPositionOffset, maxPositionOffset));
            movement.SetPositionOffset(positionOffset);
            movement.SetDestination(GridManager.Instance.GetHome());
            movement.OnArrive += ReachedHomeTile;
            if (movement.path?.Count <= 1)
                Debug.LogError($"Path not found ({name})", gameObject);

            GameManager.AddEnemy(this);
        }

        public override void ManagedUpdate()
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

            var pitch = new AudioParams.Pitch(AudioParams.Pitch.Variation.Small);
            var repetition = new AudioParams.Repetition(.05f);
            AudioController.Instance.PlaySound2D("enemy_death", .2f, pitch: pitch, repetition: repetition);

            GameManager.RemoveEnemy(this);
            Destroy(gameObject);
        }

        public override void ManagedLateUpdate()
        {
            if (!isAlive)
            {
                Die();
            }
        }
    }
}