using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITab : MonoBehaviour
{
    Button btnTab;

    void Start()
    {
        btnTab = GetComponentInChildren<Button>();
        btnTab.onClick.AddListener(OnTab);
    }

    void OnTab()
    {
        BattleManager.GetInstance().AttackMonster();
    }
}
