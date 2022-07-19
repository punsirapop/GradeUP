using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject pauseCanvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        SetPauseCanvasShow(true);
    }

    private void SetPauseCanvasShow(bool isShow)
    {
        Debug.Log("Pause Canvas is SHOW!!!");
        // pauseCanvas.SetActive(isShow);
    }
}
