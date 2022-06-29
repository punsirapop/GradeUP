using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

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
    void Update()
    {
        _Atk = FindObjectOfType<StatusManager>().Atk;
        _HP = FindObjectOfType<StatusManager>().HP;
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
    }

    public void ChangeClass(int SubClass)
    {
        ChangeSubClass?.Invoke(SubClass);
        Panal.SetActive(false);
        Time.timeScale = 1;
    }

}
