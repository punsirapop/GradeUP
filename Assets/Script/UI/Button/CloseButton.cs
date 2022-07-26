using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UIType closePanel;

    UIManager uIManager;
    Image image;

    private void Start() {
        uIManager = FindObjectOfType<UIManager>();
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (uIManager.nowCanvas == UIType.Player)
        {
            uIManager.PauseUI();
        }
        else
        {
            uIManager.CloseCanvas(closePanel);
        }

        uIManager.HoverButton(image, false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uIManager.HoverButton(image, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uIManager.HoverButton(image, false);
    }
}
