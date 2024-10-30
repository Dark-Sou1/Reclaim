using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class FireballPotion : Potion 
    {
        public float range = 2;
        public float damage = 80;


        public override void PlayPotion(Vector2 position)
        {
            foreach(Enemy e in GameManager.GetEnemies())
            {
                if (Vector2.Distance(e.transform.position, position) > range)
                    continue;

                e.Damage(damage);
            }
        }
    }

}