using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public static event Action<int> OnDrop;

    [SerializeField] GameObject pauseMenuUI;
    PlayerManager playerManager;
    StatusManager statusManager;

    [SerializeField] TextMeshProUGUI _mainClass;
    [SerializeField] TextMeshProUGUI _subClass;
    [SerializeField] List<TextMeshProUGUI> _status;
    [SerializeField] List<TextMeshProUGUI> _inventoryName;
    [SerializeField] List<Image> _inventoryImage;
    [SerializeField] List<GameObject> _inventoryActive;
    [SerializeField] protected List<Button> _inventoryButton;

    [SerializeField] GameObject dropMenuUI;
    [SerializeField] Image _itemDropImage;
    [SerializeField] TextMeshProUGUI _itemDropName;

    itemSO _itemDrop;
    int _inventoryIndex = -1;

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        statusManager = playerManager.GetComponent<StatusManager>();
    }

    void Update()
    {
        // Open - close
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1f;
                pauseMenuUI.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0f;
                pauseMenuUI.SetActive(true);
                paused = true;
            }
        }

        // Update text
        _mainClass.text = ("MainClass: " + statusManager.className);

        _status[0].SetText("HP: " + statusManager.hp);
        _status[1].SetText("ATK: " + statusManager.atk);
        _status[2].SetText("SPD: " + statusManager.spd);
        _status[3].SetText("ASPD: " + statusManager.aspd);

        for (int i = 0; i < InventoryManager.maxSlot; i++)
        {
            try
            {
                _inventoryName[i].enabled = true;
                _inventoryImage[i].enabled = true;
                _inventoryButton[i].enabled = true;
                _inventoryName[i].SetText(InventoryManager.inventory[i].ItemName);
                _inventoryImage[i].sprite = InventoryManager.inventory[i].ItemObject.GetComponent<SpriteRenderer>().sprite;
            }
            catch (System.Exception)
            {
                _inventoryName[i].enabled = false;
                _inventoryImage[i].enabled = false;
                _inventoryButton[i].enabled = false;
            }
        }
    }

    public void OnItemPress(Button button)
    {
        dropMenuUI.SetActive(true);
        _inventoryIndex = _inventoryButton.IndexOf(button);
        _itemDrop = InventoryManager.inventory[_inventoryIndex];
        _itemDropName.SetText(_itemDrop.ItemName);
        _itemDropImage.sprite = _itemDrop.ItemObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnDropAccept()
    {
        OnDrop?.Invoke(_inventoryIndex);
        _inventoryIndex = -1;
        _itemDrop = null;
    }
}
