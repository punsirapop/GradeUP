using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnamyStat")]
public class enamySO : ScriptableObject
{
    [SerializeField] private string _enamyName;
    public string EnamyName => _enamyName;

    [SerializeField] private float _HP;
    public float HP => _HP;

    [SerializeField] private float _attack;
    public float Attack => _attack;

    [SerializeField] private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;

    [SerializeField] private float _speed;
    public float Speed => _speed;
}
