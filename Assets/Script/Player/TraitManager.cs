using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct TraitStat
{
    private int _id;
    public int id => _id;

    private float _hp;
    public float hp => _hp;

    private float _atk;
    public float atk => _atk;

    private float _spd;
    public float spd => _spd;

    private float _aspd;
    public float aspd => _aspd;

    public TraitStat(int ID, float HP, float ATK, float SPD, float ASPD)
    {
        this._id = ID;
        this._hp = HP;
        this._atk = ATK;
        this._spd = SPD;
        this._aspd = ASPD;
    }

    public void UpdateTraitStat(float HP, float ATK, float SPD, float ASPD)
    {
        this._hp = HP;
        this._atk = ATK;
        this._spd = SPD;
        this._aspd = ASPD;
    }
}

public class TraitManager : MonoBehaviour
{
    public static List<int> Traits = new List<int>();
    public static List<TraitStat> traitStats = new List<TraitStat>();

    // placeholder
    public static event Action OnEnterRoom;
    public static event Action OnLeaveRoom;
    public static event Action OnAttack;
    public static event Action OnDamaged;
    public static event Action OnUseItem;
    public static event Action OnCollectMoney;
    public static event Action OnSpendMoney;
    public static event Action OnGetRewardStat;
    public static event Action OnHeal;

    public static event Action OnCollect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trait"))
        {
            int traitIndex = collision.GetComponent<TraitHandler>().traitID;
            if (!Traits.Contains(traitIndex))
            {
                Debug.Log("Trait " + traitIndex + " : get");
                AddTrait(traitIndex);
                OnCollect?.Invoke();
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Already have that trait");
            }
        }
    }

    void AddTrait(int traitID)
    {
        Traits.Add(traitID);

        switch (traitID)
        {
            case 1:
                OnEnterRoom += Trait01;
                break;
            case 2:
                TraitStat trait02 = new TraitStat(2, 10, 10, 5, 5);
                traitStats.Add(trait02);
                break;
            case 3:
                // Add money
                InventoryManager.money += 200;
                break;
            case 4:
                TraitStat trait04 = new TraitStat(4, 25, 0, 0, 0);
                traitStats.Add(trait04);
                break;
            case 5:
                float multiplier = InventoryManager.money / 1000;
                TraitStat trait05 = new TraitStat(5, 10 * multiplier, 10 * multiplier, 5 * multiplier, 5 * multiplier);
                traitStats.Add(trait05);
                OnCollectMoney += Trait05;
                OnSpendMoney += Trait05;
                break;
            case 6:
                OnGetRewardStat += Trait06;
                break;
            case 7:
                OnDamaged += Trait07;
                break;
            case 8:
                /*
                itemSO randomItem = new itemSO();
                InventoryManager.inventory.Add(randomItem);
                */
                OnDamaged += Trait08;
                break;
            case 9:
                OnHeal += Trait09;
                break;
            case 10:
                OnLeaveRoom += Trait10;
                break;
            case 11:
                TraitStat trait11 = new TraitStat(11, -10, -10, -5, -5);
                traitStats.Add(trait11);
                break;
            case 12:
                /*
                itemSO glasses = new itemSO();
                InventoryManager.inventory.Add(glasses);
                */
                OnDamaged += Trait12;
                break;
            case 13:
                OnUseItem += Trait13;
                break;
            case 14:
                /*
                itemSO evohaler = new itemSO();
                InventoryManager.inventory.Add(evohaler);
                */
                OnAttack += Trait14;
                break;
        }
    }

    // 5% chance to not spawn enemy & reward
    void Trait01()
    {
        System.Random percent = new System.Random();
        if (percent.Next(0,100) < 5)
        {
            // Skip Room
        }
    }

    // increase trait stat proportional to money
    void Trait05()
    {
        TraitStat trait05 = traitStats.Find(t => t.id == 5);
        float multiplier = InventoryManager.money / 1000;
        trait05.UpdateTraitStat(10 * multiplier, 10 * multiplier, 5 * multiplier, 5 * multiplier);

        // increase item price 20%
    }

    // increase reward stat
    // decrease giving stat
    void Trait06()
    {
        // increase reward stat
        // decrease giving stat
    }
    
    // 17% chance to enter rage mode for 3s when damaged
    void Trait07()
    {
        System.Random percent = new System.Random();
        if (percent.Next(0, 100) < 17)
        {
            StartCoroutine(Rage());
        }
    }

    // get 1 random item
    // increase received damage 20%
    void Trait08()
    {
        // give random item from whole catalog
        // receive damage +20%
    }

    // decrease received healing for 66%
    // 20% chance to get poisoned when healing
    void Trait09()
    {
        // decrease healing
        System.Random percent = new System.Random();
        if (percent.Next(0, 100) < 20)
        {
            // get poisoned
        }
    }

    // 17% to drop item when leaving room
    void Trait10()
    {
        System.Random percent = new System.Random();
        if (percent.Next(0, 100) < 17)
        {
            int itemIndex = percent.Next(0, InventoryManager.inventory.Count);
            // if item isn't undroppable item (glass, evohaler)
            InventoryManager.inventory.RemoveAt(itemIndex);
        }
    }

    // has glasses
    // 25% chance to get blurred when damaged
    void Trait12()
    {
        System.Random percent = new System.Random();
        if (percent.Next(0, 100) < 25)
        {
            // blur screen
        }
    }

    // 25% to destroy current active item
    void Trait13()
    {
        System.Random percent = new System.Random();
        if (percent.Next(0, 100) < 25)
        {
            // destroy current active item
        }
    }

    // has evohaler
    // 17% chance to unable to attack
    void Trait14()
    {
        System.Random percent = new System.Random();
        if (percent.Next(0, 100) < 17)
        {
            // unable to attack
        }
    }

    IEnumerator Rage()
    {
        TraitStat traitRage = new TraitStat(-1, 50, 50, 50, 50);
        traitStats.Add(traitRage);
        yield return new WaitForSeconds(3);
        traitStats.Remove(traitRage);
        yield break;
    }
}
