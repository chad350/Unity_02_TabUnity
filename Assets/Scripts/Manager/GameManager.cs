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


    public void AddGold(int gold)
    {
        this.gold += gold;
    }

    public bool SpendGold(int gold)
    {
        if (this.gold >= gold)
        {
            this.gold -= gold;
            return true;
        }

        return false;
    }

    public void IncreaseTotalHP(int addHp)
    {
        totalHp += addHp;
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

        // curHp = Mathf.Clamp(curHp, 0, 100);
    }
}