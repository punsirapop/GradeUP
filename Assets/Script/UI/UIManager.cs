using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum UIType
{
    Class,
    SubClass,
    Trait,
    Player,
    Pause,
    Setting,
    Death,
    Reward,
    Menu
}

public class UIManager : MonoBehaviour
{
    [Tooltip("Choose your default canvas when start this scene")]
    [SerializeField] UIType defaultStartCanvas;

    [Header("Canvas")]
    [SerializeField] GameObject classCanvas;
    [SerializeField] GameObject subClassCanvas;
    [SerializeField] GameObject traitCanvas;
    [SerializeField] GameObject playerCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject settingCanvas;
    [SerializeField] GameObject dealthCanvas;
    [SerializeField] GameObject rewardCanvas;
    [SerializeField] GameObject menuCanvas;

    [Header("Other")]
    [SerializeField] GameObject hoverTextPanel;
    
    GameObject hoverText;
    public UIType nowCanvas;

    private void Start()
    {
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

    private void ResetCanvas()
    {
        foreach (UIType type in System.Enum.GetValues(typeof(UIType)))
        {
            CloseCanvas(type);
        }
    }

    public void OpenCanvas(UIType type)
    {
        switch (type)
        {
            case UIType.Player:
                if (playerCanvas != null)
                playerCanvas.SetActive(true);
                return;
            case UIType.Pause:
                if (pauseCanvas != null)
                pauseCanvas.SetActive(true);
                return;
            case UIType.Setting:
                if (settingCanvas != null)
                settingCanvas.SetActive(true);
                return;
            case UIType.Menu:
                if (menuCanvas != null)
                MenuUI();
                return;
            default:
                return;
        }
    }

    public void CloseCanvas(UIType type)
    {
        switch (type)
        {
            case UIType.Player:
                if (playerCanvas != null)
                playerCanvas.SetActive(false);
                return;
            case UIType.Pause:
                if (pauseCanvas != null)
                pauseCanvas.SetActive(false);
                return;
            case UIType.Setting:
                if (settingCanvas != null)
                settingCanvas.SetActive(false);
                return;
            case UIType.Menu:
                if (menuCanvas != null)
                menuCanvas.SetActive(false);
                return;
            default:
                return;
        }
    }

    public void PlayUI()
    {
        nowCanvas = UIType.Player;

        Time.timeScale = 1f;
        CloseCanvas(UIType.Menu);
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

    public void MenuUI()
    {
        nowCanvas = UIType.Menu;
        Time.timeScale = 0f;
        
        ResetCanvas();
        menuCanvas.SetActive(true);
    }

    public void NextScene(int nextSceneIndex)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void HoverButton(TextMeshProUGUI text, bool isEnter)
    {
        if (text == null) return;

        if (isEnter)
        {
            text.color = new Color(.85f, .85f, .85f, 1);
        }
        else
        {
            text.color = Color.white;
        }
    }

    public void HoverButton(Image image, bool isEnter)
    {
        if (image == null) return;

        if (isEnter)
        {
            image.color = new Color(.85f, .85f, .85f, 1);
        }
        else
        {
            image.color = Color.white;
        }
    }

    public void CreateHoverText(string message)
    {
        hoverText = Instantiate(hoverTextPanel, pauseCanvas.transform);

        Vector2 mouse = Input.mousePosition;
        hoverText.transform.position = mouse;

        hoverText.GetComponent<HoverTextPanel>().SetText(message);
    }

    public void DestroyHoverText()
    {
        if (hoverText != null) Destroy(hoverText);
    }

}
