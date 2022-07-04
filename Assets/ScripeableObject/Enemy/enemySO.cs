using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyStat")]
public class enemySO : ScriptableObject
{
    [SerializeField] private string _enemyName;
    public string EnemyName => _enemyName;

    [SerializeField] private float _HP;
    public float HP => _HP;

    [SerializeField] private float _attack;
    public float Attack => _attack;

    [SerializeField] private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;

    [SerializeField] private float _speed;
    public float Speed => _speed;
}
