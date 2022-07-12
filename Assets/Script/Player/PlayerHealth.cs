using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerHealth : HealthSystem
{
    public static event Action OnPlayerDeath;

    [SerializeField] StatusManager statusManager;
    [SerializeField] TextMeshProUGUI hpDisplay;
    [SerializeField] GameObject deadDisplay;
    void Start()
    {
        characterCon.OnHit += IsHit;
        LaserGun.OnLaserHit += IsHit;

        max_HP = statusManager.HP;
        Current_HP = max_HP;

        Debug.Log("Start HP: " + Current_HP + " / " + max_HP);
    }

    void Update()
    {
        hpDisplay.SetText(Current_HP + " / " + max_HP);

        if (Current_HP <= 0)
        {
            OnPlayerDeath?.Invoke();
            hpDisplay.enabled = false;
            deadDisplay.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void IsHit(GameObject enemyAttack)
    {
        enemySO enemy = enemyAttack.GetComponent<AttackHandler>().attackOwner;
        float damage = enemy.Attack;
        GetDamage(damage);
        Debug.Log("Get attacked by " + enemy.EnemyName + " - " + damage);
        StartCoroutine(IFrame());
    }

    IEnumerator IFrame()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        Color oldColor = sr.color;

        sr.color = new Color(1, 0, 0, .5f);
        characterCon.isIFramed = true;
        yield return new WaitForSeconds(.5f);

        sr.color = oldColor;
        characterCon.isIFramed = false;
        yield break;
    }
}
