using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ClassStat")]
public class classSO : ScriptableObject
{
    [SerializeField] private string _ClassName;
    public string ClassName => _ClassName;

    [SerializeField] private float _HP;
    public float HP => _HP;

    [SerializeField] private float _Actack;
    public float Actack => _Actack;

    [SerializeField] private float _ActackSpeed;
    public float ActackSpeed => _ActackSpeed;

    [SerializeField] private float _Speed;
    public float Speed => _Speed;
}
