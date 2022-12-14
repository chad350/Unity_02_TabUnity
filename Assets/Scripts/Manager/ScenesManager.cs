using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    Battle,
    Menu,
    Main,    
}

public class ScenesManager : MonoBehaviour
{
    #region Singletone
    private static ScenesManager instance = null;

    public static ScenesManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@ScenesManager");
            instance = go.AddComponent<ScenesManager>();

            DontDestroyOnLoad(go);
        }

        return instance;
    }
    #endregion

    #region Scene Control
    public Scene currentScene;

    public void ChangeScene(Scene scene)
    {
        ResetSetting();

        currentScene = scene;        
        SceneManager.LoadScene(scene.ToString());        
    }

    void ResetSetting()
    {
        UIManager.GetInstance().ClearList();
        EffectManager.GetInstance().ReleasePool();
    }

    #endregion
}
