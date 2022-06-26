using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ClassStat")]
public class classSO : ScriptableObject
{
    [SerializeField] private string _className;
    public string className => _className;

    [SerializeField] private float _hp;
    public float hp => _hp;

    [SerializeField] private float _atk;
    public float atk => _atk;

    [SerializeField] private float _aspd;
    public float aspd => _aspd;

    [SerializeField] private float _spd;
    public float spd => _spd;
}
