using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _itemSprite;
    public Image ItemSprite => _itemSprite;
    [SerializeField] private TextMeshProUGUI _itemName;
    public TextMeshProUGUI ItemName => _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    public TextMeshProUGUI ItemDescription => _itemDescription;

    public void Setup()
    {
        Setup(null, "", "");
    }

    public void Setup(itemSO item)
    {
        Setup(item.ItemObject.GetComponent<SpriteRenderer>().sprite, item.ItemName, item.HP.ToString());
    }

    public void Setup(Item item)
    {
        Setup(item.sprite, item.name, item.description);
    }

    public void Setup(Sprite sprite, string name, string description)
    {
        if (sprite == null)
        {
            _itemSprite.sprite = null;
            _itemSprite.color = Color.clear;
        }
        else
        {
            _itemSprite.sprite = sprite;
            _itemSprite.color = Color.white;
        }
        _itemName.text = name;
        _itemDescription.text = description;
    }
}
