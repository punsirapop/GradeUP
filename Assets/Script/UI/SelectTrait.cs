using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTrait : MonoBehaviour
{
    [SerializeField] GameObject slotPrefab;
    [SerializeField] int numberOfSlot = 2;

    [SerializeField] List<string> itemsTest = new List<string>();
    
    List<int> rdmSlots = new List<int>();
    SubclassInfoSO subclassInfoSO;

    public delegate void ChangeClassDelegate(int subclass);
    public ChangeClassDelegate ChangeTrait;
    
    private void Start() {
        CreateTrait();
    }

    private void CreateTrait()
    {
        ResetUI();

        foreach (string trait in itemsTest)
        {
            GameObject subClassSlot = Instantiate(slotPrefab, transform);
            
            subClassSlot.GetComponent<SelectTraitSlot>().Setup(null, trait);
        }
    }

    private void RandomTrait()
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

    public void OnChooseTrait(int traitIndex)
    {
        GameObject.FindGameObjectWithTag("Player").gameObject.SendMessage("SubscribeChangeTrait");
        ChangeTrait?.Invoke(traitIndex);
    }
}
