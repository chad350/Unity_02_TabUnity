using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    Animator characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = ObjectManager.GetInstance().CreateCharacter();
        go.transform.localScale = new Vector3(2, 2, 2);
        go.transform.localPosition = new Vector3(0, 1.1f, 0);

        characterAnimator = go.GetComponent<Animator>();

        UIManager.GetInstance().SetEventSystem();
        UIManager.GetInstance().OpenUI("UIProfile");
        UIManager.GetInstance().OpenUI("UIActionMenu");
    }
}
