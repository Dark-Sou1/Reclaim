using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class MoveOverTime : MonoBehaviour
    {
        public Vector3 speed = Vector3.right;

        void Update()
        {
            transform.position += speed * Time.deltaTime;
        }
    }
}