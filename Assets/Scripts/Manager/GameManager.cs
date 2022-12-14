using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singletone
    private static GameManager instance = null;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@GameManager");
            instance = go.AddComponent<GameManager>();

            DontDestroyOnLoad(go);
        }

        return instance;
    }
    #endregion

    public string playerName = "Chad";

    public int level = 1;

    public int gold = 500; // 추가, 삭제

    public int totalHp = 100; // 증가
    public int curHp = 100; // 증가, 감소

    public void LoadData()
    {
        playerName = PlayerPrefs.GetString("playerName", "Chad");

        level = PlayerPrefs.GetInt("level", 1);
        gold = PlayerPrefs.GetInt("gold", 500);
        totalHp = PlayerPrefs.GetInt("totalHp", 100);
        curHp = PlayerPrefs.GetInt("curHp", 100);
    }

    // key            level  gold    totalHp    curHp
    // value            1    500     100        100


    public void SaveData()
    {
        PlayerPrefs.SetString("playerName" , playerName);

        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("totalHp", totalHp);
        PlayerPrefs.SetInt("curHp", curHp);
    }

    public void AddGold(int gold)
    {
        this.gold += gold;
        SaveData();
    }

    public bool SpendGold(int gold)
    {
        if (this.gold >= gold)
        {
            this.gold -= gold;
            SaveData();
            return true;
        }

        return false;
    }

    public void IncreaseTotalHP(int addHp)
    {
        totalHp += addHp;
        SaveData();
    }

    public void SetCurrentHP(int hp)
    {
        curHp += hp;

        //   110  >  100
        if (curHp > totalHp)
            curHp = totalHp; // cur -> 100

        //    -20 < 0
        if (curHp < 0)
            curHp = 0;  // cur -> 0

        SaveData();
        // curHp = Mathf.Clamp(curHp, 0, 100);
    }
}