using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    protected float max_HP, Current_HP;

    protected void SetMaxHP(float _max_HP)
    {
        this.max_HP = _max_HP;
    }

    protected void SetHp(float hp)
    {
        this.Current_HP = hp;
        Debug.Log("set HP" + Current_HP);
    }

    public float GetHp()
    {
        return this.Current_HP;
    }

    protected void GetDamage(float _damage)
    {
        this.Current_HP -= _damage;
    }
    protected void GetHeal(float amount)
    {
        this.Current_HP += amount;
    }
    protected IEnumerator GetOvertimeDamage(float _damage, int time)
    {
        for (int i = 1; i <= time; i++)
        {
            GetDamage(_damage);
            PrintHP();
            yield return new WaitForSeconds(1f);
        }
    }

    protected void PrintHP()
    {
        Debug.Log(gameObject.tag + " HP Left : " + Current_HP + " / " + max_HP);
    }
}
