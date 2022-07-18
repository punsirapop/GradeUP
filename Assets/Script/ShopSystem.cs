using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> Item;
    [SerializeField] private GameObject[] ItemSpawner;
    [SerializeField] private GameObject ItemSpawnerTemplete;

    public delegate void SpawnItemDelegate(GameObject Item);
    public SpawnItemDelegate SetItem;

    private void Start()
    {
        Item = new List<GameObject>(Resources.LoadAll<GameObject>("Prefab/Item"));
        ItemSpawner = GameObject.FindGameObjectsWithTag("ItemSpawner");
        SpawnItem();
    }
    private void SpawnItem()
    {
        foreach(GameObject Spawner in ItemSpawner)
        {
            Instantiate(ItemSpawnerTemplete, Spawner.transform.position, Quaternion.identity);
            int rand = UnityEngine.Random.Range(0, Item.Count);
            SetItem?.Invoke(Item[rand]);
            Item.RemoveAt(rand);
        }
    }
}
