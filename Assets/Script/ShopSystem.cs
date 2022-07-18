using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> Item;
    [SerializeField] private GameObject[] ItemSpawner;
    private void Start()
    {
        Item = new List<GameObject>(Resources.LoadAll<GameObject>("Prefab/Item"));//Resources.LoadAll<GameObject>("Prefab/Item");
        ItemSpawner = GameObject.FindGameObjectsWithTag("ItemSpawner");
        SpawnItem();
    }


    private void SpawnItem()
    {
        foreach(GameObject Spawner in ItemSpawner)
        {
            int rand = UnityEngine.Random.Range(0, Item.Count);
            Instantiate(Item[rand], Spawner.transform.position , Quaternion.identity);
            Item.RemoveAt(rand);
        }
    }
}
