using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    Player,
    Pause,
    Setting,
    Menu
}

public class UIManager : MonoBehaviour
{
    [SerializeField] UIType defaultStartCanvas;

    [SerializeField] GameObject playerCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject settingCanvas;
    [SerializeField] GameObject menuCanvas;

    private void Start() {
        foreach (UIType type in System.Enum.GetValues(typeof(UIType)))
        {
            if (type == defaultStartCanvas)
            {
                OpenCanvas(type);
            }
            else
            {
                CloseCanvas(type);
            }
        }
    }

    public void OpenCanvas(UIType type)
    {
        switch (type)
        {
            case UIType.Player:
                playerCanvas.SetActive(true);
                return;
            case UIType.Pause:
                pauseCanvas.SetActive(true);
                return;
            case UIType.Setting:
                settingCanvas.SetActive(true);
                return;
            // case UIType.Menu:
            //     menuCanvas.SetActive(true);
            //     return;
            default:
                return;
        }
    }

    public void CloseCanvas(UIType type)
    {
        switch (type)
        {
            case UIType.Player:
                playerCanvas.SetActive(false);
                return;
            case UIType.Pause:
                pauseCanvas.SetActive(false);
                return;
            case UIType.Setting:
                settingCanvas.SetActive(false);
                return;
            // case UIType.Menu:
            //     menuCanvas.SetActive(false);
            //     return;
            default:
                return;
        }
    }

    public void PlayUI()
    {
        Time.timeScale = 1f;
        CloseCanvas(UIType.Pause);
        OpenCanvas(UIType.Player);
    }

    public void PauseUI()
    {
        Time.timeScale = 0f;
        CloseCanvas(UIType.Player);
        CloseCanvas(UIType.Setting);
        OpenCanvas(UIType.Pause);
    }

    public void SettingUI()
    {
        CloseCanvas(UIType.Pause);
        OpenCanvas(UIType.Setting);
    }

    public void HoverButton(Image image, bool isEnter)
    {
        if (image == null) return;

        // Debug.Log(image?.gameObject + " is Hover : " + isEnter);
        if (isEnter)
        {
            image.color = new Color(.85f, .85f, .85f, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }
}
