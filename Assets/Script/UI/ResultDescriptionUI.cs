using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultDescriptionUI : MonoBehaviour
{
    [Header("Grade Text")]
    [SerializeField] TextMeshProUGUI nameGradeText;
    [SerializeField] TextMeshProUGUI classGradeText;
    [SerializeField] TextMeshProUGUI pysicGradeText;
    [SerializeField] TextMeshProUGUI chemGradeText;
    [SerializeField] TextMeshProUGUI peGradeText;

    [Space]
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] Image resultImage;

    [Header("Win Result")]
    [SerializeField] string winText = "PASS";
    [SerializeField] Sprite winSrite;

    [Header("Lose Result")]
    [SerializeField] string loseText = "FAIL";
    [SerializeField] Sprite loseSrite;
    
    bool isWin;

    public void Setup(bool isWin)
    {
        this.isWin = isWin;

        if (isWin)
        {
            resultText.text = winText;
            resultImage.sprite = winSrite;
        }
        else
        {
            
            resultText.text = loseText;
            resultImage.sprite = loseSrite;
        }
    }
}
