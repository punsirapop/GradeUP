using System;
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
    [SerializeField] GameObject Panal;
    private float _Atk;
    private float _HP;
    private float _Atk_Speed;
    private float _Movespeed;

    public delegate void ChangeClassDelegate(int subclass);
    public ChangeClassDelegate ChangeSubClass;

    public delegate void GetDamageDelegate(float HP);
    public GetDamageDelegate GetDamage;
    public delegate IEnumerator GetDamageOvertimeDelegate(float HP,int sec);
    public GetDamageOvertimeDelegate GetOvertimeDamage;
    void Update()
    {
        _Atk = FindObjectOfType<StatusManager>().Atk;
        _HP = FindObjectOfType<HealthSystem>().Current_HP;
        _Atk_Speed = FindObjectOfType<StatusManager>().Atk_Speed;
        _Movespeed = FindObjectOfType<StatusManager>().MoveSpeed;

        Atk.SetText("Atk : " + _Atk);
        HP.SetText("HP : " + _HP);
        Atk_Speed.SetText("Aspd : " + _Atk_Speed);
        _movespeed.SetText("MoveSpeed : " + _Movespeed);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Panal.activeInHierarchy == false)
            {

                Panal.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetDamage?.Invoke(40f);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(GetOvertimeDamage(15f,3));
        }
        if(_HP == 0)
        {
            StopAllCoroutines();
        }
    }

    public void ChangeClass(int SubClass)
    {
        ChangeSubClass?.Invoke(SubClass);
        Panal.SetActive(false);
        Time.timeScale = 1;
    }

}
