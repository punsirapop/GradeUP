using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemStat")]
public class itemSO : ScriptableObject
{
    [SerializeField] private string _itemName;
    public string ItemName => _itemName;

    [SerializeField] private GameObject _itemObject;
    public GameObject ItemObject => _itemObject;

    [SerializeField] private float _HP;
    public float HP => _HP;

    [SerializeField] private float _attack;
    public float Attack => _attack;

    [SerializeField] private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private bool _isActive;
    public bool isActive => _isActive;

    [SerializeField] private int _price = 20;
    public int Price => _price;
}
