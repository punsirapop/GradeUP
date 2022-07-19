using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private GameObject Item, FloatText;
    [SerializeField] private Transform Itempoint, Textpoint;

    public delegate void OnBuyDelegate(int amount);
    public OnBuyDelegate OnBuy;

    private void Awake()
    {
        FindObjectOfType<ShopSystem>().SetItem += SetShopItem;
    }
    private void SetShopItem(GameObject Items)
    {
        FindObjectOfType<ShopSystem>().SetItem -= SetShopItem;
        this.Item = Instantiate(Items, Itempoint.transform.position, Quaternion.identity, this.transform);
        price = Item.GetComponent<ItemScript>().Price;
        Item.GetComponent<BoxCollider2D>().enabled = false;
        this.FloatText.GetComponent<TextMesh>().text = price.ToString();
        Instantiate(FloatText, Textpoint.transform.position, Quaternion.identity, this.transform);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<MoneyManager>().currentMoney >= price)
            {
                Debug.Log("Purchase Item");
                MoneyManager.Instance.UseMoney(price);
                //Add Item to Inventory
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
    }
}
