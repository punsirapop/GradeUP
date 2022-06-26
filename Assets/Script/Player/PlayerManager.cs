using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] StatusManager statusManager;
    [SerializeField] InventoryManager inventoryManager;

    void Start()
    {
        statusManager = GetComponent<StatusManager>();
        inventoryManager = GetComponent<InventoryManager>();
    }
}
