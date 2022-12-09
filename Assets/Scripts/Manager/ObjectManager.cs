using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    #region Singletone
    private static ObjectManager instance = null;

    public static ObjectManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@ObjectManager");
            instance = go.AddComponent<ObjectManager>();

            DontDestroyOnLoad(go);
        }

        return instance;
    }
    #endregion

    public GameObject CreateCharacter()
    {
        Object characterObj = Resources.Load("Sprite/Character");
        GameObject character = (GameObject)Instantiate(characterObj);

        return character;
    }

    public GameObject CreateMonster()
    {
        Object monsterObj = Resources.Load("Sprite/Monster1");
        GameObject monster = (GameObject)Instantiate(monsterObj);

        return monster;
    }

    public ParticleSystem CreateHitEffect()
    {
        Object effectObj = Resources.Load("Effect/Hit_Green");
        GameObject effect = (GameObject)Instantiate(effectObj);

        return effect.GetComponent<ParticleSystem>();
    }
}
