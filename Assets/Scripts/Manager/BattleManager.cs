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

        UIManager.GetInstance().OpenUI("UITab");

        StartCoroutine("BattleProgress");
    }

    // 2~3 �� �ð� ������ ���Ͱ� ���� ����
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

            Debug.Log($"���Ͱ� �÷��̾�� ������ �߽��ϴ� - ������ : {damage}     ���� ü�� : {GameManager.GetInstance().curHp}");
        }

        Lose();
    }

    public void AttackMonster()
    {
        float randX = Random.Range(-1.5f, 1.5f);
        float randY = Random.Range(-1.5f, 1.5f);

        var particle = ObjectManager.GetInstance().CreateHitEffect();
        particle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        particle.transform.localPosition = new Vector3(0 + randX, 0.7f + randY, -0.5f);

        monsterData.hp--;

        if (monsterData.hp <= 0)
        {
            Victory();
        }
    }

    void Victory()
    {
        Debug.Log("���ӿ��� �¸��߽��ϴ�.");
        StopCoroutine("BattleProgress");
        UIManager.GetInstance().CloseUI("UITab");

        GameManager.GetInstance().AddGold(monsterData.gold);

        Invoke("MoveToMain", 2.5f);
    }

    void Lose()
    {
        Debug.Log("���ӿ��� �й��߽��ϴ�.");
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