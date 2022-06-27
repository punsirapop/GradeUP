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

    [SerializeField] private bool _isActivable;
    public bool isActivable => _isActivable;

    [SerializeField] private float _hp;
    public float hp => _hp;

    [SerializeField] private float _atk;
    public float atk => _atk;

    [SerializeField] private float _spd;
    public float spd => _spd;

    [SerializeField] private float _aspd;
    public float aspd => _aspd;
}
