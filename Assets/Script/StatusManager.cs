using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    //status public for Debug only
    [SerializeField] classSO ClassStatus;
    public float Atk,HP,Atk_Speed,MoveSpeed,Art;
    //public float HP;
    //public float Atk_Speed;
    //public float MoveSpeed;
    //public int Art;

    //public int Atk_p, HP_p, Atk_Speed_p, MoveSpeed_p;
    private void Start()
    {
        this.MoveSpeed = ClassStatus.Speed;
        this.Atk = ClassStatus.Attack;
        this.HP = ClassStatus.HP;
        this.Atk_Speed = ClassStatus.AttackSpeed;
        //Art = ClassStatus.Speed;
    }

    //update stat

}
