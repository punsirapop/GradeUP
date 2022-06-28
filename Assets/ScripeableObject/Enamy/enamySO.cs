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

    [SerializeField] private float _actack;
    public float Actack => _actack;

    [SerializeField] private float _actackSpeed;
    public float ActackSpeed => _actackSpeed;

    [SerializeField] private float _speed;
    public float Speed => _speed;
}
