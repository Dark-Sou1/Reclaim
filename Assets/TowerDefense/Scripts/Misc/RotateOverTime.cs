using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class RotateOverTime : MonoBehaviour
    {
        public float speed;
        public float offset;

        void Update()
        {
            transform.rotation = Quaternion.Euler(0, 0, Time.time * speed + offset);
        }
    }
}