using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image itemSprite;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemDescription;

    public void Setup()
    {
        Setup(null, "", "");
    }

    public void Setup(Sprite sprite, string name, string description)
    {
        itemSprite.sprite = sprite;
        itemName.text = name;
        itemDescription.text = description;
    }
}
