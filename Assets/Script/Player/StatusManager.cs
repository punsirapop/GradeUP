using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusManager : MonoBehaviour
{
    [SerializeField] protected classSO playerClass;

    string _className;
    public string className => _className;

    float _atk;
    public float atk => _atk;

    float _hp;
    public float hp => _hp;

    float _aspd;
    public float aspd => _aspd;

    float _spd;
    public float spd => _spd;

    int Art;

    void Start()
    {
        InventoryManager.OnCollect += AddItemStat;
        PauseMenu.OnDrop += RemoveItemStat;
        _className = playerClass.className;
        _atk = playerClass.atk;
        _hp = playerClass.hp;
        _aspd = playerClass.aspd;
        _spd = playerClass.spd;
    }

    void AddItemStat(GameObject itemHolder)
    {
        itemSO item = itemHolder.GetComponent<ItemHandler>().item;
        Debug.Log("Adding stats...");
        _atk += item.atk;
        _hp += item.hp;
        _spd += item.spd;
        _aspd += item.aspd;
    }

    void RemoveItemStat(itemSO item)
    {
        _atk -= item.atk;
        _hp -= item.hp;
        _spd -= item.spd;
        _aspd -= item.aspd;
    }
}
