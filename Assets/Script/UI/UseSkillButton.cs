using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UseSkillButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI countdownText;

    bool isCooldown;
    [SerializeField] int cooldownDelay = 5;
    [SerializeField] int nowCooldownDelay = 5;

    Coroutine countdownSkill;

    public void OnEnable()
    {
        if (isCooldown)
        {
            countdownSkill = StartCoroutine(CountdownSkill(nowCooldownDelay));
        }
    }

    private void OnDisable() {
        if (countdownSkill != null)
        {
            StopCoroutine(countdownSkill);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCooldown)
        {
            countdownSkill = StartCoroutine(CountdownSkill(cooldownDelay));
        }
    }

    IEnumerator CountdownSkill(int cooldownDelay)
    {
        SetIsCooldown(true);
        nowCooldownDelay = cooldownDelay;
        for (int i = cooldownDelay; i > 0; i--)
        {
            countdownText.text = i.ToString();
            nowCooldownDelay = i;
            yield return new WaitForSeconds(1f);
        }
        countdownSkill = null;
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
