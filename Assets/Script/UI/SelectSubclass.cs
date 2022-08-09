using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSubclass : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab;
    [SerializeField] int numberOfSlot = 2;

    [SerializeField] List<string> itemsTest = new List<string>();
    
    List<int> rdmSlots = new List<int>();
    SubclassInfoSO subclassInfoSO;

    public delegate void ChangeClassDelegate(int subclass);
    public ChangeClassDelegate ChangeSubClass;
    
    private void Start() {
        CreateSubclass();
    }

    private void CreateSubclass()
    {
        ResetUI();

        subclassInfoSO = MainGame.instance.playerController.ClassStatus.Subclass;
        RandomSubClass();

        foreach (int subclassIndex in rdmSlots)
        {
            GameObject subClassSlot = Instantiate(slotPrefab, transform);
            
            subClassSlot.GetComponent<SelectSubclassSlot>().Setup(subclassInfoSO.getName(subclassIndex), subclassInfoSO.getPic(subclassIndex),  subclassInfoSO.getDesc(subclassIndex), subclassIndex + 1);
        }
    }

    private void RandomSubClass()
    {
        System.Random rdm = new System.Random();

        while (rdmSlots.Count < numberOfSlot)
        {
            int rdmIndex = rdm.Next(3);
            if (!rdmSlots.Contains(rdmIndex))
            {
                rdmSlots.Add(rdmIndex);
            }
        }
    }

    private void ResetUI()
    {
        foreach (Component child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnChooseSubclass(int subclassIndex)
    {
        GameObject.FindGameObjectWithTag("Player").gameObject.SendMessage("SubscribeChangeSubClass");
        ChangeSubClass?.Invoke(subclassIndex);
    }
}
