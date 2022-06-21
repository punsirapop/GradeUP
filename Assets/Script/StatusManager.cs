using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    //status
    [SerializeField] classSO ClassStatus;
    protected float Atk;
    protected float HP;
    protected float Atk_Speed;
    protected float _movespeed;
    protected int Art;

    private void Start()
    {
        this._movespeed = ClassStatus.Speed;
        this.Atk = ClassStatus.Actack;
        this.HP = ClassStatus.HP;
        this.Atk_Speed = ClassStatus.ActackSpeed;
        //Art = ClassStatus.Speed;
    }
}
