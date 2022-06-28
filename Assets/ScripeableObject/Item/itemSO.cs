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

    [SerializeField] private float _actack;
    public float Actack => _actack;

    [SerializeField] private float _actackSpeed;
    public float ActackSpeed => _actackSpeed;

    [SerializeField] private float _speed;
    public float Speed => _speed;
}
