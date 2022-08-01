using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SubclassTraitSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] string slotType;

    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI text;

    UIManager uIManager;

    public void Setup(string type, Sprite image, string text)
    {
        if (image != null) iconImage.sprite = image;
        
        slotType = type;
        this.text.text = text;

        uIManager = FindObjectOfType<UIManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotType == "Subclass")
        {
            Debug.Log($"You choose subclass : {text.text}!");

            uIManager.TraitUI();
        }
        else
        {
            Debug.Log($"You choose trait : {text.text}!");

            uIManager.PlayUI();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector2(1.1f, 1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector2(1, 1);
    }
}
