using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[System.Serializable]
public class TraitTest
{
    public string traitName;
    public Sprite traitSprite;
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
    [SerializeField] SubClassSprite[] subClassSprites = new SubClassSprite[5];
    [SerializeField] Image subClassImage;
    [SerializeField] TextMeshProUGUI subClassNameText;

    [Header("Trait Slot")]
    [SerializeField] TraitSlot[] traitSlots = new TraitSlot[3];

    [Header("Inventory Slot")]
    [SerializeField] InventorySlot[] inventorySlots = new InventorySlot[5];

    Dictionary<ClassType, List<Sprite>> subClassSpritesDict = new Dictionary<ClassType, List<Sprite>>();

    private Sprite characterSprite;
    private Sprite classSprite;
    private Sprite subClassSprite;

    UIManager uIManager;

    private void Start() {
        // Debug.Log("Pause UI Enable!");

        uIManager = FindObjectOfType<UIManager>();
        UpdateUI();
    }

    private void Awake() {
        foreach (SubClassSprite subClassSprite in subClassSprites)
        {
            subClassSpritesDict.Add(subClassSprite.type, subClassSprite.sprites);
        }
    }

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
        CharacterCon player = MainGame.instance.playerController;

        roomNoText.text = uIManager.roomName;
        classNameText.text =  player.ClassStatus.ClassName;

        subClassNameText.text =  GetSubClassName(player.ActiveSubClass);
        subClassImage.sprite = subClassSprite;
        if (subClassSprite != null)
        {
            subClassImage.color = Color.white;
        }
        else
        {
            subClassImage.color = Color.clear;
        }

    }

    private string GetSubClassName(int activeSubClass)
    {
        if (activeSubClass < 1 || activeSubClass > 3)
        {
            subClassSprite = null;
            return "None";
        }

        SubclassInfoSO subclassInfo = MainGame.instance.playerController.ClassStatus.Subclass;

        subClassSprite = subclassInfo.getPic(activeSubClass - 1);
        return subclassInfo.getName(activeSubClass - 1);
    }

    // Debug
    [Header("DEBUG!!!")]
    [SerializeField] TraitTest[] nowTrait = new TraitTest[3];

    private void UpdateTraitUI()
    {
        // ResetTraitUI();
        
        for (int i = 0; i < nowTrait.Length; i++)
        {
            TraitTest trait = nowTrait[i];

            traitSlots[i].Setup(trait);
        }
    }

    private void ResetTraitUI()
    {
        foreach(TraitSlot slot in traitSlots)
        {
            slot.Setup();
        }
    }

    // Debug
    [SerializeField] itemSO[] inventory = new itemSO[5];

    private void UpdateInventoryUI()
    {
        ResetInventoryUI();
        for (int i = 0; i < inventory.Length; i++)
        {
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

    public enum ClassType
    {
        Art,
        Linguistic,
        PE,
        Physics,
        Chemistry
    }

    [Serializable]
    public class SubClassSprite
    {
        public ClassType type;
        public List<Sprite> sprites = new List<Sprite>(3);
    }
}
