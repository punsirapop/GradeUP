using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    //status public for Debug only
    [SerializeField] classSO ClassStatus;
    public float Atk, HP, Atk_Speed, MoveSpeed, Art;

    private float Atk_base, HP_base, Atk_Speed_base, MoveSpeed_base;
    private int Atk_p = 100, HP_p = 100, Atk_Speed_p = 100, MoveSpeed_p = 100;
    private int Atk_f, HP_f, Atk_Speed_f, MoveSpeed_f;

    public delegate void SetMaxHPDelegate(float HP);
    public SetMaxHPDelegate SetMaxHP;
    private void Start()
    {
        this.HP_base = ClassStatus.HP;
        this.Atk_base = ClassStatus.Attack;
        this.Atk_Speed_base = ClassStatus.AttackSpeed;
        this.MoveSpeed_base = ClassStatus.Speed;
        UpdateStat();
    }

    public void AddPercentBonus(int hp, int atk, int atk_speed, int speed) //Add bonus %
    {
        HP_p += hp;
        Atk_p += atk;
        Atk_Speed_p += atk_speed;
        MoveSpeed_p += speed;
        UpdateStat();
    }

    public void AddFlatBonus(int hp, int atk, int atk_speed, int speed) //Add bonus flat 
    {
        HP_f += hp;
        Atk_f += atk;
        Atk_Speed_f += atk_speed;
        MoveSpeed_f += speed;
        UpdateStat();
    }

    private void UpdateStat() //Calculate&Update Final Stat
    {
        HP = (HP_base + HP_f) * HP_p / 100;
        SetMaxHP?.Invoke(HP);
        Atk = (Atk_base + Atk_f) * Atk_p / 100;
        Atk_Speed = (Atk_Speed_base + Atk_Speed_f) * Atk_Speed_p / 100;
        MoveSpeed = (MoveSpeed_base + MoveSpeed_f) * MoveSpeed_p / 100;
    }
}
