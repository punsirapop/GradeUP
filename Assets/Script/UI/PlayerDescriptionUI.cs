using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDescriptionUI : MonoBehaviour
{
    [Header("Player Description")]
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI roomNoText;
    [SerializeField] TextMeshProUGUI moneyAmountText;
    [SerializeField] TextMeshProUGUI classNameText;
    [SerializeField] TextMeshProUGUI subClassNameText;

    [Header("Trait Slot")]
    [SerializeField] Image[] traitSlots = new Image[3];

    [Header("Inventory Slot")]
    [SerializeField] InventorySlot[] inventorySlots = new InventorySlot[5];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
