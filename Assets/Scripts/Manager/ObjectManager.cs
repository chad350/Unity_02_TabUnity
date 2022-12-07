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
}
