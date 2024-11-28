using UnityEngine;
using UnityEngine.UI;
namespace Giacomo
{
    [RequireComponent(typeof(Image))]
    public class PotionProgressBar : MonoBehaviour
    {
        public float widthWhenCooldown = .1f;
        public float widthWhenReady = .05f;

        [SerializeField]
        protected Potion potion;

        protected Image outlineImage;
        protected bool cooldownOver = true;

        private void Awake()
        {
            outlineImage = GetComponent<Image>();
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
        }

        protected void UpdateProgress()
        {
            var progress = 1 - (potion.NextAvailableTime - Time.time) / potion.cooldown;
            
            if(progress < 1)
            {
                outlineImage.fillAmount = progress;
            }
            else
            {
                outlineImage.fillAmount = 1;
                cooldownOver = true;
            }
        }
    }

}