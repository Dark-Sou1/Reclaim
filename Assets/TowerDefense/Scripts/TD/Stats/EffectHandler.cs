using Giacomo;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    public Dictionary<string, ActiveEffect> effects;


    private void Awake()
    {
        effects = new();
    }

    public void AddEffect(string name, Effect effect, float duration, bool forceUpdateDuration = false)
    {
        if (effects.ContainsKey(name)) 
        {
            if (effects[name].activeUntil < Time.time + duration || forceUpdateDuration)
                effects[name].activeUntil = Time.time + duration;
            return;
        }

        effects.Add(name, new ActiveEffect(effect, Time.time + duration));
        effect.handler = this;
        effects[name].StartEffect();
    }

    public void RemoveEffect(string name)
    {
        if (!effects.ContainsKey(name))
            return;
        effects[name].EndEffect();
        effects.Remove(name);
    }

    float nextTick;
    private void Update()
    {
        if (Time.time < nextTick) return;
        nextTick = Time.time + (1f / GameManager.gameTickFrequency);

        List<string> toRemove = new List<string>();
        foreach (var effect in effects)
        {
            if (effect.Value.activeUntil < Time.time)
            {
                toRemove.Add(effect.Key);
                continue;
            }

            effect.Value.UpdateEffect();
        }

        //avoids changing effects list while iterating it
        foreach (var effect in toRemove)
            RemoveEffect(effect);
    }

    public class ActiveEffect
    {
        public float activeUntil;
        public Effect effect;

        public void StartEffect() => effect.StartEffect();
        public void UpdateEffect() => effect.UpdateEffect();
        public void EndEffect() => effect.EndEffect();

        public ActiveEffect(Effect effect, float duration) 
        {
            this.effect = effect;
            this.activeUntil = duration;
        }
    }
}


public abstract class Effect
{
    public EffectHandler handler;
    public virtual void StartEffect() { }
    public virtual void UpdateEffect() { }
    public virtual void EndEffect() { }
}
