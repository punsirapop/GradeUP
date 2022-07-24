using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventoryManager : MonoBehaviour
{
    public static List<itemSO> inventory = new List<itemSO>();
    public static int maxSlot = 5;
    int _activeItemIndex = -1;

    private void Awake()
    {
        CharacterCon.OnCollectItem += CollectItem;
    }

    void CollectItem(GameObject itemHolder)
    {
        if (inventory.Count < maxSlot)
        {
            inventory.Add(itemHolder.GetComponent<ItemScript>().ItemStat);
            Destroy(itemHolder);
            Debug.Log("Inventory Count: " + inventory.Count);
        }
        else
        {
            Debug.Log("Inventory Full: " + inventory.Count);
        }
    }

    void DropItem(int index)
    {
        inventory.RemoveAt(index);
    }
}