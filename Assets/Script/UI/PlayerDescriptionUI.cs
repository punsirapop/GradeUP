using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// Debug
[Serializable]
public class Item
{
    public Sprite sprite;
    public string name;
    public string description;
}

public class PlayerDescriptionUI : MonoBehaviour
{
    [Header("Player Description")]
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI roomNoText;
    [SerializeField] TextMeshProUGUI moneyAmountText;
    [SerializeField] Image classImage;
    [SerializeField] TextMeshProUGUI classNameText;
    [SerializeField] Image subClassImage;
    [SerializeField] TextMeshProUGUI subClassNameText;

    [Header("Trait Slot")]
    [SerializeField] Image[] traitSlots = new Image[3];

    [Header("Inventory Slot")]
    [SerializeField] InventorySlot[] inventorySlots = new InventorySlot[5];

    private void OnEnable() {
        Debug.Log("Pause UI Enable!");
        UpdateUI();
    }

    // private void Start() {
    //     Debug.Log("Pause Start!");
    //     UpdateUI();
    // }

    private void Update() {
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateProfileUI();
        UpdateTraitUI();
        UpdateInventoryUI();
    }

    private void UpdateProfileUI()
    {
        characterCon player = MainGame.instance.playerController;

        classNameText.text =  player.ClassStatus.ClassName;

        subClassNameText.text =  GetSubClassName(player.ActiveSubClass);
    }

    private string GetSubClassName(int activeSubClass)
    {
        switch (activeSubClass)
        {
            default:
                return activeSubClass.ToString();
        }
    }

    private void UpdateTraitUI()
    {
        foreach(Image slot in traitSlots)
        {

        }
    }

    // Debug
    // [SerializeField] Item[] inventory = new Item[5];
    [SerializeField] itemSO[] inventory = new itemSO[5];

    private void UpdateInventoryUI()
    {
        ResetInventoryUI();
        for (int i = 0; i < inventory.Length; i++)
        {
            // Item item = inventory[i];
            itemSO item = inventory[i];
            if (item == null || item.name == "") return;

            inventorySlots[i].Setup(item);
        }
    }

    private void ResetInventoryUI()
    {
        foreach(InventorySlot inventorySlot in inventorySlots)
        {
            inventorySlot.Setup();
        }
    }
}
