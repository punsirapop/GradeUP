using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    //status public for Debug only
    [SerializeField] classSO ClassStatus;
    public float Atk;
    public float HP;
    public float Atk_Speed;
    public float _movespeed;
    public int Art;

    private void Start()
    {
        this._movespeed = ClassStatus.Speed;
        this.Atk = ClassStatus.Actack;
        this.HP = ClassStatus.HP;
        this.Atk_Speed = ClassStatus.ActackSpeed;
        //Art = ClassStatus.Speed;
    }
}
