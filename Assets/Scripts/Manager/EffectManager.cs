using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    #region Singletone
    private static EffectManager instance = null;

    public static EffectManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@EffectManager");
            instance = go.AddComponent<EffectManager>();

            DontDestroyOnLoad(go);
        }

        return instance;
    }
    #endregion

    Stack<ParticleSystem> effectPool = new Stack<ParticleSystem>();

    public void InitEffectPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var effect = ObjectManager.GetInstance().CreateHitEffect();
            effect.gameObject.SetActive(false);
            effectPool.Push(effect);
        }        
    }

    public void ReleasePool()
    {
        effectPool.Clear();
    }

    public void UseEffect()
    {
        ParticleSystem effect = null;

        if (effectPool.Count > 0)
        {
            effect = effectPool.Pop();
            effect.gameObject.SetActive(true);          
        }
        else
        {
            effect = ObjectManager.GetInstance().CreateHitEffect();
        }

        effect.Play();

        float randX = Random.Range(-1.5f, 1.5f);
        float randY = Random.Range(-1.5f, 1.5f);

        effect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        effect.transform.localPosition = new Vector3(0 + randX, 0.7f + randY, -0.5f);
    }

    public void ReturnEffect(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
        effectPool.Push(particle);
    }
}
