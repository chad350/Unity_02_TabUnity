using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    void Start()
    {
        UIManager.GetInstance().SetEventSystem();
        UIManager.GetInstance().OpenUI("UIMainMenu");       
    }
}
