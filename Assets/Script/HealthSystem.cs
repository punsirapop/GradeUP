using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public float max_HP, Current_HP;
    [SerializeField] DebugUI DebugUI;
    private void OnEnable()
    {
        GetComponent<StatusManager>().SetMaxHP += SetMaxHP;
        DebugUI.GetDamage += GetDamage;
        DebugUI.GetOvertimeDamage += GetOvertimeDamage;
    }
    private void OnDisable()
    {
        GetComponent<StatusManager>().SetMaxHP -= SetMaxHP;
        DebugUI.GetDamage -= GetDamage;
        DebugUI.GetOvertimeDamage -= GetOvertimeDamage;
    }
    protected void SetMaxHP(float _max_HP)
    {
        this.max_HP = _max_HP;
        Current_HP = max_HP;
    }
    protected void GetDamage(float _damage)
    {
        if (Current_HP - _damage > 0)
        {
            this.Current_HP -= _damage;
        }
        else
        {
            Current_HP = 0;
            Debug.Log("You died");
        }
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
            yield return new WaitForSeconds(1f);
        }
    }
}
