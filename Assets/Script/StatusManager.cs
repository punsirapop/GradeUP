using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    //status
    [SerializeField] classSO ClassStatus;
    public float Atk;
    public float HP;
    public float Atk_Speed;
    public float _movespeed;
    public int Art;

    private void Start()
    {
        _movespeed = ClassStatus.Speed;
        Atk = ClassStatus.Actack;
        HP = ClassStatus.HP;
        Atk_Speed = ClassStatus.ActackSpeed;
        //Art = ClassStatus.Speed;
    }
}
