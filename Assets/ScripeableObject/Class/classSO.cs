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

    [SerializeField] private float _Attack;
    public float Attack => _Attack;

    [SerializeField] private float _AttackSpeed;
    public float AttackSpeed => _AttackSpeed;

    [SerializeField] private float _Speed;
    public float Speed => _Speed;

    [SerializeField] private SubclassInfoSO _Subclass;
    public SubclassInfoSO Subclass => _Subclass;

}
