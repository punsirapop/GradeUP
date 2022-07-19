using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;

    private void OnEnable() {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealthBar(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
