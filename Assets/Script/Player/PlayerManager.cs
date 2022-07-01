using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static event Action<Collider2D> OnTrigger;
    [SerializeField] StatusManager statusManager;
    [SerializeField] InventoryManager inventoryManager;

    void Start()
    {
        statusManager = GetComponent<StatusManager>();
        inventoryManager = GetComponent<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger?.Invoke(collision);
    }
}
