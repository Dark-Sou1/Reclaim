using UnityEditor;
using UnityEngine;

namespace Giacomo
{
    [RequireComponent(typeof(CircularLineRenderer))]
    public class PotionCastCursor : MonoBehaviour
    {
        public Potion potion;
        protected CircularLineRenderer circularLineRenderer;

        private void Start()
        {
            circularLineRenderer = GetComponent<CircularLineRenderer>();
            if(potion == null)
                potion = GetComponentInParent<Potion>();

            float range = 100;
            if(potion is FireballPotion)
            {
                range = (potion as FireballPotion).range;
            }                
            else if (potion is PoisonPotion)
            {
                range = (potion as PoisonPotion).range;
            }

            transform.localScale = new Vector3(range, range);
        }

        private void Update()
        {
            Vector3 pos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }
    }

}