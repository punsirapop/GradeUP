using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    UIManager uIManager;
    Image image;
    TextMeshProUGUI text;

    private void Start() {
        uIManager = FindObjectOfType<UIManager>();
        image = GetComponent<Image>();
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uIManager.MenuUI();

        OnButtonHover(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnButtonHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnButtonHover(false);
    }

    public void OnButtonHover(bool isEnter)
    {
        uIManager.HoverButton(image, isEnter);
        uIManager.HoverButton(text, isEnter);
    }
}
