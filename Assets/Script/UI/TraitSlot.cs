using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TraitSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TraitTest trait;
    
    UIManager uIManager;

    public void Setup()
    {
        Setup(null);
    }

    public void Setup(TraitTest trait)
    {
        uIManager = FindObjectOfType<UIManager>();
        this.trait = trait;
        
        if (trait == null)
        {
            GetComponent<Image>().sprite = null;
        }
        else
        {
            GetComponent<Image>().sprite = trait.traitSprite;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        uIManager.CreateHoverText(trait.traitName + " " + trait.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uIManager.DestroyHoverText();
    }
}
