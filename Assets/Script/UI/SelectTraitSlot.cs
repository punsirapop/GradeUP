using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectTraitSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] TextMeshProUGUI descriptionText;

    UIManager uIManager;
    int traitIndex;

    public delegate void ChangeClassDelegate(int subclass);
    public ChangeClassDelegate ChangeSubClass;

    private void Start() {
        
    }

    public void Setup(Sprite image, string description)
    {
        Setup("", image, description, 0);
    }

    public void Setup(string name, Sprite image, string description, int activeSubclass)
    {
        traitIndex = activeSubclass;

        if (image != null) iconImage.sprite = image;
        
        this.headerText.text = name;
        this.descriptionText.text = description;

        uIManager = FindObjectOfType<UIManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"You choose trait : {headerText.text}!");
        uIManager.PlayUI();
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
