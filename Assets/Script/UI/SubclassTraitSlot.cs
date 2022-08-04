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
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] TextMeshProUGUI descriptionText;

    UIManager uIManager;
    int subclassIndex;

    public delegate void ChangeClassDelegate(int subclass);
    public ChangeClassDelegate ChangeSubClass;

    private void Start() {
        
    }

    public void Setup(string type, Sprite image, string description)
    {
        Setup(type, "", image, description, 0);
    }

    public void Setup(string type, string name, Sprite image, string description, int activeSubclass)
    {
        subclassIndex = activeSubclass;

        if (image != null) iconImage.sprite = image;
        
        slotType = type;
        this.headerText.text = name;
        this.descriptionText.text = description;

        uIManager = FindObjectOfType<UIManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotType == "Subclass")
        {
            GetComponentInParent<SelectSubclassTrait>().OnChooseSubclass(subclassIndex);
            uIManager.TraitUI();
        }
        else
        {
            Debug.Log($"You choose trait : {headerText.text}!");
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
