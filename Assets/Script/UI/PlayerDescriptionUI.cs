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

    private void Start() {
        Debug.Log("Pause UI Enable!");
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
        characterCon player = MainGame.instance.playerController;

        classNameText.text =  player.ClassStatus.ClassName;

        subClassNameText.text =  GetSubClassName(player.ClassStatus.ClassName, player.ActiveSubClass);
        if (subClassSprite != null) subClassImage.sprite = subClassSprite;

    }

    private string GetSubClassName(string className, int activeSubClass)
    {
        if (activeSubClass < 1 || activeSubClass > 3)
        {
            return "None";
        }

        if (className == ClassType.Art.ToString())
        {
            subClassSprite = subClassSpritesDict[ClassType.Art][activeSubClass-1];
            switch (activeSubClass)
            {
                case 1:
                    return "Color Attack"; // ไม่มีผสมสี แต่ฟันสีหลักติดเอฟเฟคเลย
                case 2:
                    return "Long Shot"; // ปืนยิงสีระยะกลาง กระสุนหายไปเมื่อสุดระยะ
                case 3:
                    return "Dash Attack"; // พุ่งไปข้างหน้าพร้อมทําดาเมจใส่ศัตรูที่ผ่าน
                default:
                    return "None";
            }
        }
        else if (className == ClassType.Chemistry.ToString())
        {
            subClassSprite = subClassSpritesDict[ClassType.Chemistry][activeSubClass-1];
            switch (activeSubClass)
            {
                case 1:
                    return "Explosion"; // Flat dmg + knockback
                case 2:
                    return "Poison"; // ทําดาเมจ DoT ใส่เป้าหมายภายระยะระเบิด
                case 3:
                    return "Burn"; // พื้นติดไฟ ทําความเสียหายใส่ศัตรูที่ยืนบนพื้น
                default:
                    return "None";
            }
        }
        else if (className == ClassType.Linguistic.ToString())
        {
            subClassSprite = subClassSpritesDict[ClassType.Linguistic][activeSubClass-1];
            switch (activeSubClass)
            {
                case 1:
                    return "Cone Up"; // เพิ่มระยะ Cone ให้กว้าง-ไกลขึ้น
                case 2:
                    return "Alphabet Cicle"; // วงตัวหนังสือหมุนวนรอบตัวแล้วทําความเสียหายเมื่อศัตรูชน
                case 3:
                    return "Character Stun"; // สตั้นศัตรูทั้งแมพเป็นระยะเวลาสั้น ๆ พร้อมทําความเสียหายเล็กน้อย
                default:
                    return "None";
            }
        }
        else if (className == ClassType.PE.ToString())
        {
            subClassSprite = subClassSpritesDict[ClassType.PE][activeSubClass-1];
            switch (activeSubClass)
            {
                case 1:
                    return "Doomfist"; // ชาร์จต่อย - ช้า แรง มีพุ่ง มี knockback
                case 2:
                    return "Fast Punch"; // ต่อยรัว - ตีเร็วและรัว knockback เล็กน้อย
                case 3:
                    return "Baseball Bat"; // ไม้เบสบอล - เพิ่มระยะโจมตี มี knockback
                default:
                    return "None";
            }
        }
        else if (className == ClassType.Physics.ToString())
        {
            subClassSprite = subClassSpritesDict[ClassType.Physics][activeSubClass-1];
            switch (activeSubClass)
            {
                case 1:
                    return "Three Shot"; // ยิงสามทาง
                case 2:
                    return "Quick Shot"; // ยิงรัว
                case 3:
                    return "Bouncing Bullet"; // กระสุนกระเด้งกำแพง
                default:
                    return "None";
            }
        }

        return "";
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
