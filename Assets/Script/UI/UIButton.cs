using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum ButtonType
{
    Open,
    Close,
    OpenAndClose,
    Play,
    Setting,
    Menu,
    Pause,
    Exit,
    NextScene,
    ResetLevel
}

public class UIButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ButtonType type;

    [Header("When choose Type Open or Close")]
    [SerializeField] UIType openCanvas;
    [SerializeField] UIType closeCanvas;

    [Header("When choose NextScene")]
    [SerializeField] int nextSceneIndex;

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
        OnButtonClick();

        OnButtonHover(false);
    }

    private void OnButtonClick()
    {
        switch (type)
        {
            case ButtonType.Open:
                uIManager.OpenCanvas(openCanvas);
                return;
            case ButtonType.Close:
                uIManager.CloseCanvas(closeCanvas);
                return;
            case ButtonType.OpenAndClose:
                uIManager.CloseCanvas(closeCanvas);
                uIManager.OpenCanvas(openCanvas);
                return;
            case ButtonType.Pause:
                uIManager.PauseUI();
                return;
            case ButtonType.Play:
                uIManager.PlayUI();
                return;
            case ButtonType.Setting:
                uIManager.SettingUI();
                return;
            case ButtonType.Menu:
                uIManager.MenuUI();
                return;
            case ButtonType.NextScene:
                uIManager.NextScene(nextSceneIndex);
                return;
            case ButtonType.ResetLevel:
                uIManager.ResetLevel();
                return;
            case ButtonType.Exit:
                Application.Quit();
                return;
            default:
                return;
        }
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
