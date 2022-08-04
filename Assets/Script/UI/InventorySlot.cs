using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public void Setup()
    {
        Setup(null, "", "");
    }

    public void Setup(itemSO item)
    {
        List<string> description = new List<string>();
        
        if (item.HP != 0)
        {
            description.Add($"+ {item.HP} HP");
        }
        if (item.Attack != 0)
        {
            description.Add($"+ {item.Attack} Attack");
        }
        if (item.AttackSpeed != 0)
        {
            description.Add($"+ {item.AttackSpeed} AtkSpeed");
        }
        if (item.Speed != 0)
        {
            description.Add($"+ {item.Speed} Speed");
        }

        Setup(item.ItemObject?.GetComponent<SpriteRenderer>().sprite, item.ItemName, string.Join(", ", description));
    }

    public void Setup(Sprite sprite, string name, string description)
    {
        if (sprite == null)
        {
            itemSprite.sprite = null;
            itemSprite.color = Color.clear;
        }
        else
        {
            itemSprite.sprite = sprite;
            itemSprite.color = Color.white;
        }
        itemName.text = name;
        itemDescription.text = description;
    }
}
