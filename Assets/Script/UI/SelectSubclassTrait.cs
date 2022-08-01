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

    [SerializeField] List<string> items = new List<string>();
    
    private void Start() {
        CreateSubclass();
    }

    private void CreateSubclass()
    {
        ResetUI();

        foreach (string subclass in items)
        {
            SubclassTraitSlot subClassSlot = Instantiate(slotPrefab, transform).GetComponent<SubclassTraitSlot>();
            
            subClassSlot.Setup(type.ToString(), null, subclass);
        }
    }

    private void ResetUI()
    {
        foreach (Component child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
