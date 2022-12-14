using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region Singletone
    private static BattleManager instance = null;

    public static BattleManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@BattleManager");
            instance = go.AddComponent<BattleManager>();

            DontDestroyOnLoad(go);
        }

        return instance;
    }
    #endregion

    public Monster1 monsterData;
    public void BattleStart(Monster1 monster)
    {
        monsterData = monster;

        EffectManager.GetInstance().InitEffectPool(10);

        UIManager.GetInstance().OpenUI("UITab");

        StartCoroutine("BattleProgress");
    }

    // 2~3 초 시간 가지고 몬스터가 저를 공격
    IEnumerator BattleProgress()
    {
        while (GameManager.GetInstance().curHp > 0)
        {
            yield return new WaitForSeconds(monsterData.delay);

            int damage = monsterData.atk;
            GameManager.GetInstance().SetCurrentHP(-damage);

            GameObject ui = UIManager.GetInstance().GetUI("UIProfile");
            if (ui != null)
                ui.GetComponent<UIProfile>().RefreshState();

            Debug.Log($"몬스터가 플레이어에게 공격을 했습니다 - 데미지 : {damage}     남은 체력 : {GameManager.GetInstance().curHp}");
        }

        Lose();
    }

    public void AttackMonster()
    {     
        EffectManager.GetInstance().UseEffect();

        monsterData.hp--;

        if (monsterData.hp <= 0)
        {
            Victory();
        }
    }

    void Victory()
    {
        Debug.Log("게임에서 승리했습니다.");
        StopCoroutine("BattleProgress");
        UIManager.GetInstance().CloseUI("UITab");

        GameManager.GetInstance().AddGold(monsterData.gold);

        Invoke("MoveToMain", 2.5f);
    }

    void Lose()
    {
        Debug.Log("게임에서 패배했습니다.");
        UIManager.GetInstance().CloseUI("UITab");
        
        if(GameManager.GetInstance().SpendGold(500))
            GameManager.GetInstance().SetCurrentHP(80);
        else
            GameManager.GetInstance().SetCurrentHP(10);

        Invoke("MoveToMain", 2.5f);
    }

    void MoveToMain()
    {
        ScenesManager.GetInstance().ChangeScene(Scene.Main);
    }
}
