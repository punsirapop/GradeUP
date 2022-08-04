using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ResultDescriptionUI : MonoBehaviour
{
    [Header("Grade")]
    [SerializeField] string nameGrade = "F";
    [SerializeField] string classGrade = "F";
    [SerializeField] string physicGrade = "F";
    [SerializeField] string chemGrade = "F";
    [SerializeField] string peGrade = "F";

    [Header("Grade Text")]
    [SerializeField] TextMeshProUGUI nameGradeText;
    [SerializeField] TextMeshProUGUI classGradeText;
    [SerializeField] TextMeshProUGUI physicGradeText;
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
            nameGrade = "A";
            classGrade = "A";
            physicGrade = "A";
            chemGrade = "A";
            peGrade = "A";

            resultText.text = winText;
            resultImage.sprite = winSrite;
        }
        else
        {
            nameGrade = "F";
            classGrade = "F";
            physicGrade = "F";
            chemGrade = "F";
            peGrade = "F";
            
            resultText.text = loseText;
            resultImage.sprite = loseSrite;
        }
        SetGradeUI();
    }

    private void SetGradeUI()
    {
        nameGradeText.text = nameGrade;
        classGradeText.text = classGrade;
        physicGradeText.text = physicGrade;
        chemGradeText.text = chemGrade;
        peGradeText.text = peGrade;
    }
}
