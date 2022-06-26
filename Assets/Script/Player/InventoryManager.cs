using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventoryManager : MonoBehaviour
{
    public static event Action<GameObject> OnCollect;
    public static List<itemSO> inventory = new List<itemSO>();
    public static int maxSlot = 5;
    int _activeItem = -1;

    private void Start()
    {
        PauseMenu.OnDrop += DropItem;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Debug.Log("Calling Actions...");
            if (inventory.Count < maxSlot)
            {
                OnCollect?.Invoke(collision.gameObject);
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("Inventory Full");
            }
        }
    }

    void DropItem(itemSO item)
    {
        inventory.Remove(item);
    }
}