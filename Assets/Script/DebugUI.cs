using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugUI : MonoBehaviour
{
    [SerializeField] TMP_Text Atk;
    [SerializeField] TMP_Text HP;
    [SerializeField] TMP_Text Atk_Speed;
    [SerializeField] TMP_Text _movespeed;

    private float _Atk;
    private float _HP;
    private float _Atk_Speed;
    private float _Movespeed;
    void Update()
    {
        _Atk = FindObjectOfType<StatusManager>().atk;
        _HP = FindObjectOfType<StatusManager>().hp;
        _Atk_Speed = FindObjectOfType<StatusManager>().aspd;
        _Movespeed = FindObjectOfType<StatusManager>().spd;

        Atk.SetText("Atk : " + _Atk);
        HP.SetText("HP : " + _HP);
        Atk_Speed.SetText("Aspd : " + _Atk_Speed);
        _movespeed.SetText("MoveSpeed : " + _Movespeed);
    }

}
