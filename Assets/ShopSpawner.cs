using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Item;
    [SerializeField] private GameObject FloatText;
    [SerializeField] private int price;

    [SerializeField] private Transform Itempoint;
    [SerializeField] private Transform Textpoint;
    private void Awake()
    {
        FindObjectOfType<ShopSystem>().SetItem += SetShopItem;
    }

    private void SetShopItem(GameObject Items)
    {
        FindObjectOfType<ShopSystem>().SetItem -= SetShopItem;
        Debug.Log("Spawn" + Items.name);
        this.Item = Instantiate(Items, Itempoint.transform.position , Quaternion.identity,this.transform);
        Item.GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(FloatText, Textpoint.transform.position, Quaternion.identity, this.transform);
    }
}
