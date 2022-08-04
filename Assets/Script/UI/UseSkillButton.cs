using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class UseSkillButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI countdownText;

    bool isCooldown;
    [SerializeField] int cooldownDelay = 5;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCooldown)
        {
            StartCoroutine(CountdownSkill());
        }
    }

    IEnumerator CountdownSkill()
    {
        SetIsCooldown(true);
        for (int i = cooldownDelay; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        SetIsCooldown(false);
    }

    private void SetIsCooldown(bool isCooldown)
    {
        this.isCooldown = isCooldown;
        if (isCooldown)
        {
            countdownText.gameObject.SetActive(true);
            backgroundImage.color = Color.gray;
            iconImage.color = Color.gray;
        }
        else
        {
            countdownText.gameObject.SetActive(false);
            backgroundImage.color = Color.white;
            iconImage.color = Color.white;
        }
    }
}
