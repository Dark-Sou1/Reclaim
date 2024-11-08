using UnityEngine;
namespace Giacomo
{
    [RequireComponent(typeof(CircularLineRenderer))]
    public class PotionProgressBar : MonoBehaviour
    {
        public float widthWhenCooldown = .1f;
        public float widthWhenReady = .05f;

        [SerializeField]
        protected Potion potion;
        
        protected CircularLineRenderer lineRenderer;
        protected bool cooldownOver = true;

        private void Awake()
        {
            lineRenderer = GetComponent<CircularLineRenderer>();
            if (potion == null) 
                potion = GetComponentInParent<Potion>();
            potion.OnUsed += OnPotionUsed;
        }
        private void Start()
        {
            UpdateProgress();
        }

        private void Update()
        {
            if (!cooldownOver)
                UpdateProgress();
        }

        protected void OnPotionUsed()
        {
            cooldownOver = false;
            lineRenderer.DrawCircle(1, width: widthWhenCooldown);
        }

        protected void UpdateProgress()
        {
            var progress = 1 - (potion.NextAvailableTime - Time.time) / potion.cooldown;
            if(progress < 1)
            {
                if(progress > 0)
                    lineRenderer.DrawCircle(progress);
            }
            else
            {
                lineRenderer.DrawCircle(1, width: widthWhenReady);
                cooldownOver = true;
            }
        }
    }

}