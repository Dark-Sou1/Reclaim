using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class Projectile : MonoBehaviour
    {
        [DisableContextMenu] public float damage;
        [DisableInEditorMode] public float speed;
        [DisableInEditorMode] public float lifetime;
        [DisableInEditorMode] public float splashArea;
        [DisableInEditorMode] public Targetable target;
        [DisableInEditorMode] public bool destroyIfTargetDied;

        public GameObject impactEffect;

        protected Vector3 lastTargetPosition;
        private bool isInitialized;
        public void Initialize(float damage, float speed, float lifetime, float splashArea, Targetable target, bool destroyIfTargetDied)
        {
            isInitialized = true;
            this.damage = damage;
            this.speed = speed;
            this.lifetime = lifetime;
            this.splashArea = splashArea;
            this.target = target;
            this.destroyIfTargetDied = destroyIfTargetDied;
        }

        private float despawnTime;
        private void Start()
        {
            if (!isInitialized)
                Destroy(gameObject);
            despawnTime = Time.time + lifetime;
        }

        private void Update()
        {
            if (Time.time >= despawnTime)
            {
                Destroy(gameObject);
                return;
            }

            if (target == null || !target.isAlive)
            {
                if (destroyIfTargetDied)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            else
            {
                lastTargetPosition = target.transform.position;
            }

            
            if (MoveTowards(lastTargetPosition))
            {
                TargetHit();
                Destroy(gameObject);
            }
        }

        public bool MoveTowards(Vector3 targetPosition)
        {
            bool reached = false;
            Vector3 direction = (targetPosition - transform.position).normalized;
            var moveby = direction * speed * Time.deltaTime;

            if (Vector3.Distance(targetPosition, transform.position) < speed * Time.deltaTime)
                reached = true;

            transform.up = direction;
            transform.position += moveby;
            return reached;
        }


        protected void TargetHit()
        {
            GameObject impactGO = null;
            if(impactEffect)
                impactGO = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(impactGO, .2f);

            if (splashArea <= 0)
            {
                target.Damage(damage);
            }
            else
            {
                var hit = Physics2D.CircleCastAll(transform.position, splashArea, Vector2.zero);
                foreach (var h in hit)
                {
                    var targetable = h.transform.GetComponent<Targetable>();
                    if (targetable)
                        targetable.Damage(damage);
                }

                if(impactGO)
                    impactGO.transform.localScale = Vector3.one * splashArea;
            }
        }

    }
}