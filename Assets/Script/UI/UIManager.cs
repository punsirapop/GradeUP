using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum UIType
{
    SubClass,
    Trait,
    Player,
    Pause,
    Setting,
    Win,
    Lose,
    Reward,
    Menu
}

public class UIManager : MonoBehaviour
{
    [Tooltip("Choose your default canvas when start this scene")]
    [SerializeField] UIType defaultStartCanvas;
    [SerializeField] public string roomName = "0-0";

    [Header("Canvas GameObject")]
    [SerializeField] GameObject classCanvas;
    [SerializeField] GameObject subClassCanvas;
    [SerializeField] GameObject traitCanvas;
    [SerializeField] GameObject playerCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject settingCanvas;
    [SerializeField] GameObject resultCanvas;
    [SerializeField] GameObject rewardCanvas;
    [SerializeField] GameObject menuCanvas;

    [Header("Other")]
    [SerializeField] GameObject hoverTextPanel;
    
    GameObject hoverText;
    public UIType nowCanvas;

    public int nowSceneIndex;

    private void Start()
    {
        nowSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
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

    private void Update()
    {
        InputHandle();
    }

    private void InputHandle()
    {
        if (nowCanvas == UIType.Player)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseUI();
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                WinResultUI();
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                LoseResultUI();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                PlayUI();
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
        nowCanvas = type;
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
            case UIType.SubClass:
                if (subClassCanvas != null)
                SubclassUI();
                return;
            case UIType.Trait:
                if (traitCanvas != null)
                traitCanvas.SetActive(true);
                return;
            case UIType.Win:
                if (resultCanvas != null)
                WinResultUI();
                return;
            case UIType.Lose:
                if (resultCanvas != null)
                LoseResultUI();
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
            case UIType.SubClass:
                if (subClassCanvas != null)
                subClassCanvas.SetActive(false);
                return;
            case UIType.Trait:
                if (traitCanvas != null)
                traitCanvas.SetActive(false);
                return;
            case UIType.Win:
                if (resultCanvas != null)
                resultCanvas.SetActive(false);
                return;
            case UIType.Lose:
                if (resultCanvas != null)
                resultCanvas.SetActive(false);
                return;
            default:
                return;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void PlayUI()
    {
        ResetCanvas();

        nowCanvas = UIType.Player;

        ResumeGame();
        OpenCanvas(UIType.Player);
    }

    public void PauseUI()
    {
        PauseGame();

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
        PauseGame();
        
        ResetCanvas();
        menuCanvas.SetActive(true);
    }

    public void SubclassUI()
    {
        ResetCanvas();

        PauseGame();
        subClassCanvas.SetActive(true);
    }

    public void TraitUI()
    {
        ResetCanvas();

        PauseGame();
        OpenCanvas(UIType.Trait);
    }

    public void WinResultUI()
    {
        ResetCanvas();
        PauseGame();

        resultCanvas.SetActive(true);

        resultCanvas.GetComponent<ResultDescriptionUI>().Setup(true);
    }

    public void LoseResultUI()
    {
        ResetCanvas();
        PauseGame();

        resultCanvas.SetActive(true);

        resultCanvas.GetComponent<ResultDescriptionUI>().Setup(false);
    }

    public void NextScene(int nextSceneIndex)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(nowSceneIndex);
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
