using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemHandler : MonoBehaviour
{
    public itemSO item;
    void Awake()
    {
        InventoryManager.OnCollect += AddItem;
    }

    void OnDestroy()
    {
        InventoryManager.OnCollect -= AddItem;
    }

    void AddItem(GameObject itemHolder)
    {
        if(itemHolder == gameObject)
        {
            itemSO item = itemHolder.GetComponent<ItemHandler>().item;
            InventoryManager.inventory.Add(item);
        }
    }
}
