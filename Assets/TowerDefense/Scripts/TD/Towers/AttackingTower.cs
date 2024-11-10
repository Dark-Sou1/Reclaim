using UnityEngine;

namespace Giacomo
{
    public class AttackingTower : Tower
    {
        
        [Header("Tower Stats")]
        public float b_rotationSpeed = 180;
        public float b_maxRange = 3;
        public float b_minRange = 0;

        [Header("Attack")]
        public float b_damage = 5;
        public float b_attackSpeed = 1;

        [Header("Advanced")]
        public Transform cannon;
        public Transform maxRangeIndicator;
        public Transform minRangeIndicator;
        public float attackAngleThreshold = 5;
        public bool rotateTowardsTarget = true;

        [Header("Runtime")]
        public Targetable target;


        protected override void Initialize()
        {
            stats.AddStat("rotationSpeed", b_rotationSpeed, 0);
            stats.AddStat("maxRange", b_maxRange, 0);
            stats.AddStat("minRange", b_minRange, 0);
            stats.AddStat("damage", b_damage, 0);
            stats.AddStat("attackSpeed", b_attackSpeed, 0);
            stats["maxRange"].OnValueChanged += UpdateRangeIndicators;
            stats["minRange"].OnValueChanged += UpdateRangeIndicators;
            UpdateRangeIndicators(null);
        }

        protected void UpdateRangeIndicators(Stat.StatValueChangedEventArgs args)
        {
            if (maxRangeIndicator)
            {
                float max = stats["maxRange"] * 2;
                maxRangeIndicator.localScale = new Vector3(max, max);
            }
            if (minRangeIndicator)
            {
                float min = stats["minRange"] * 2;
                minRangeIndicator.localScale = new Vector3(min, min);
            }
        }


        private void Update()
        {
            UpdateAttack();
        }

        private float nextShotTime;
        private bool hadTargetLastFrame;
        protected virtual void UpdateAttack()
        {
            if (!IsValidTarget(target))
            {
                if (hadTargetLastFrame)
                    OnTargetLost();
                hadTargetLastFrame = false;

                FindNewTarget();

                if (target == null)
                    return;

                OnTargetFound();
                hadTargetLastFrame = true;
            }

            RotateTowards(target.transform.position);

            if (!FacingTarget()) return;

            if (Time.time < nextShotTime) return;
            nextShotTime = Time.time + 1 / stats["attackSpeed"];

            Attack();
        }

        protected virtual void Attack()
        {

        }

        protected virtual bool IsValidTarget(Targetable t)
        {
            if (t == null) return false;
            if (!t.isAlive) return false;

            float distance = Vector3.Distance(t.transform.position, transform.position);
            if (distance > stats["maxRange"] || distance < stats["minRange"])
                return false;

            return true;
        }

        protected bool FacingTarget()
        {
            Vector3 directionToTarget = target.transform.position - cannon.position;
            float angleToTarget = Vector3.Angle(cannon.transform.up, directionToTarget);
            return angleToTarget < attackAngleThreshold;
        }

        protected virtual void RotateTowards(Vector3 target)
        {
            Vector3 direction = target - cannon.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);

            cannon.rotation = Quaternion.Slerp(cannon.rotation, targetRotation, stats["rotationSpeed"] * Time.deltaTime);
        }

        protected virtual bool FindNewTarget()
        {
            Enemy bestEnemy = null;
            float bestDistance = float.MaxValue;
            foreach (Enemy e in GameManager.Enemies)
            {
                if (!IsValidTarget(e))
                    continue;

                float enemyDist = e.movement.DistanceFromTarget();
                if (enemyDist < bestDistance)
                {
                    bestDistance = enemyDist;
                    bestEnemy = e;
                }
            }

            target = bestEnemy;
            return target != null;
        }

        protected virtual void OnTargetFound() { }
        protected virtual void OnTargetLost() { }


        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, stats["maxRange"]);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, stats["minRange"]);
            }
            else
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, b_maxRange);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, b_minRange);
            }
        }
    }
}