using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{

    public class BoostTower : Tower
    {
        public string modifierName;
        public List<BoostTowerUpgrade> buffs;

        protected float boostDuration = 1;
        protected float boostFrequency = 2;


        protected float nextBoostTime;
        public override void ManagedUpdate()
        {
            if (Time.time < nextBoostTime) return;
            nextBoostTime = Time.time + 1f / boostFrequency;

            foreach (var t in GridManager.Instance.GetAdjacentTiles(GridManager.FixCoordinates(transform.position)))
            {
                var tower = t.GetComponentInChildren<AttackingTower>();
                if (!tower) continue;


                StatModifierEffect boostEffect = new StatModifierEffect(modifierName, tower.stats);
                foreach(var buff in buffs)
                    boostEffect.AddModifier(buff.stat, buff.add, buff.multiply);
                

                tower.effects?.AddEffect("boostTowerEffect", boostEffect, boostDuration);
            }
        }

        [Serializable]
        public class BoostTowerUpgrade
        {
            public string stat;
            public float add;
            public float multiply;
        }
    }

}