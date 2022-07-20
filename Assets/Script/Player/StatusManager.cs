using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    //status public for Debug only
    [SerializeField] public classSO ClassStatus;

    public float Atk, HP, Atk_Speed, MoveSpeed, Art;

    private int Atk_base, HP_base, Atk_Speed_base, MoveSpeed_base;
    private int Atk_p, HP_p, Atk_Speed_p, MoveSpeed_p;
    private int Atk_f, HP_f, Atk_Speed_f, MoveSpeed_f;

    [SerializeField] protected int activeSubClass = 0;
    public int ActiveSubClass => activeSubClass;

    protected void InitializeStats()
    {
        this.HP = ClassStatus.HP;
        this.Atk = ClassStatus.Attack;
        this.Atk_Speed = ClassStatus.AttackSpeed;
        this.MoveSpeed = ClassStatus.Speed;
        //Art = ClassStatus.Speed;
    }

    private void AddPercentBonus(int hp, int atk , int atk_speed, int speed) //Add bonus %
    {
        HP_p += hp;
        Atk_p += atk;
        Atk_Speed_p += atk_speed;
        MoveSpeed_p += speed;
        UpdateStat();
    }

     private void AddFlatBonus(int hp, int atk, int atk_speed, int speed) //Add bonus flat 
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
        Atk = (Atk_base + Atk_f) * Atk_p / 100;
        Atk_Speed = (Atk_Speed_base + Atk_Speed_f) * Atk_Speed_p / 100;
        MoveSpeed = (MoveSpeed_base + MoveSpeed_f) * MoveSpeed_p / 100;
    }

    protected virtual void ChangeSubClass(int ID) //for Change Sub-Class 
    {
        switch (ID)
        {
            case 0: //default
                activeSubClass = 0;
                break;

            case 1: //Explotion
                activeSubClass = 1;
                break;

            case 2: //Posion
                activeSubClass = 2;
                break;

            case 3: //Burn
                activeSubClass = 3;
                break;

            default:
                break;
        }
        //FindObjectOfType<DebugUI>().ChangeSubClass -= ChangeSubClass; //when debug finish remove comment plz   
    }
}
