using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem Instance;
    [SerializeField] public float max_HP, Current_HP;
    [SerializeField] DebugUI DebugUI;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
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
    public void GetDamage(float _damage)
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
    public void GetHeal(float amount)
    {
        this.Current_HP += amount;
    }
    public IEnumerator GetOvertimeDamage(float _damage, int time)
    {
        for (int i = 1; i <= time; i++)
        {
            GetDamage(_damage);
            yield return new WaitForSeconds(1f);
        }
    }
}
