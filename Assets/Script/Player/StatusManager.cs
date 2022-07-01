using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusManager : MonoBehaviour
{
    [SerializeField] protected classSO playerClass;

    string _className;
    public string className => _className;

    float _atkBase;
    float _atkReward;
    float _atkItem;
    float _atkTrait;
    float _atkFinal => _atkBase + _atkReward + _atkItem + _atkTrait;
    public float atk => _atkFinal;

    float _hpBase;
    float _hpReward;
    float _hpItem;
    float _hpTrait;
    float _hpFinal => _hpBase + _hpReward + _hpItem + _hpTrait;
    public float hp => _hpFinal;

    float _aspdBase;
    float _aspdReward;
    float _aspdItem;
    float _aspdTrait;
    float _aspdFinal => _aspdBase + _aspdReward + _aspdItem + _aspdTrait;
    public float aspd => _aspdFinal;

    float _spdBase;
    float _spdReward;
    float _spdItem;
    float _spdTrait;
    float _spdFinal => _spdBase + _spdReward + _spdItem + _spdTrait;
    public float spd => _spdFinal;

    int Art;

    void Start()
    {
        InventoryManager.OnCollect += UpdateStat;
        PauseMenu.OnDrop += UpdateStat;
        _className = playerClass.className;
        _atkBase = playerClass.atk;
        _hpBase = playerClass.hp;
        _aspdBase = playerClass.aspd;
        _spdBase = playerClass.spd;
    }

    public void AddReward()
    {

    }

    public void UpdateStat()
    {
        Debug.Log("Updating status...");

        // Item
        _atkItem = 0;
        _hpItem = 0;
        _aspdItem = 0;
        _spdItem = 0;
        
        foreach (itemSO item in InventoryManager.inventory)
        {
            _atkItem += item.atk;
            _hpItem += item.hp;
            _aspdItem += item.aspd;
            _spdItem += item.spd;
        }

        _atkItem *= (_atkBase + _atkReward) / 100;
        _hpItem *= (_hpBase + _hpReward) / 100;
        _aspdItem *= (_aspdBase + _aspdReward) / 100;
        _spdItem *= (_spdBase + _spdReward) / 100;

        // Trait
        _atkTrait = 0;
        _hpTrait = 0;
        _aspdTrait = 0;
        _spdTrait = 0;

        foreach (TraitStat trait in TraitManager.traitStats)
        {
            _atkItem += trait.atk;
            _hpItem += trait.hp;
            _aspdItem += trait.aspd;
            _spdItem += trait.spd;
        }

        _atkTrait *= (_atkBase + _atkReward) / 100;
        _hpTrait *= (_hpBase + _hpReward) / 100;
        _aspdTrait *= (_aspdBase + _aspdReward) / 100;
        _spdTrait *= (_spdBase + _spdReward) / 100;
    }
}
