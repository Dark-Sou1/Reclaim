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

            float range = potion.range;

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