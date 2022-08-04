using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSubclassTrait : MonoBehaviour
{
    private enum SlotType {
        Subclass,
        Trait
    }

    [SerializeField] SlotType type;
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

        if (SlotType.Subclass == type)
        {
            subclassInfoSO = MainGame.instance.playerController.ClassStatus.Subclass;
            RandomSubClass();

            foreach (int subclassIndex in rdmSlots)
            {
                GameObject subClassSlot = Instantiate(slotPrefab, transform);
                
                subClassSlot.GetComponent<SubclassTraitSlot>().Setup(type.ToString(), subclassInfoSO.getName(subclassIndex), subclassInfoSO.getPic(subclassIndex),  subclassInfoSO.getDesc(subclassIndex), subclassIndex + 1);
            }
        }
        else
        {
            foreach (string subclass in itemsTest)
            {
                GameObject subClassSlot = Instantiate(slotPrefab, transform);
                
                subClassSlot.GetComponent<SubclassTraitSlot>().Setup(type.ToString(), null, subclass);
            }
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
