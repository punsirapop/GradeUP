using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerHealth : HealthSystem
{
    public static event Action OnPlayerDeath;

    enemySO enemy;

    [SerializeField] StatusManager statusManager;
    [SerializeField] TextMeshProUGUI hpDisplay;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject deadDisplay;

    int poisonStack = 0;

    void Start()
    {
        characterCon.OnHit += IsHit;
        characterCon.OnPoisoned += IsPoisoned;
        LaserGun.OnLaserHit += IsHit;

        max_HP = statusManager.HP;
        Current_HP = max_HP;
        
        // Debug.Log("Start HP: " + Current_HP + " / " + max_HP);
        healthBar.SetMaxHealthBar(max_HP);
    }

    void Update()
    {
        // hpDisplay.SetText(Current_HP + " / " + max_HP);
        healthBar.SetHealth(Current_HP);

        if (Current_HP <= 0)
        {
            OnPlayerDeath?.Invoke();
            // hpDisplay.enabled = false;
            deadDisplay.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void IsHit(GameObject enemyAttack)
    {
        try
        {
            enemy = enemyAttack.GetComponent<AttackHandler>().attackOwner;
        }
        catch (Exception)
        {
            enemy = enemyAttack.GetComponent<EnemyHealth>().enemyStat;
        }

        float damage = enemy.Attack;
        GetDamage(damage);
        Debug.Log("Get attacked by " + enemy.EnemyName + " - " + damage);
        StartCoroutine(IFrame());
    }

    void IsPoisoned(GameObject enemyAttack)
    {
        enemy = enemyAttack.GetComponent<AttackHandler>().attackOwner;
        float damage = enemy.Attack;
        Debug.Log("Get poisoned by " + enemy.EnemyName + " - " + damage + " ("+ poisonStack + ")");
        poisonStack++;
        StartCoroutine(GetOvertimeDamage(damage, 4));
        StartCoroutine(ReducePoisonStack(4));
        StartCoroutine(IFrame());
    }

    IEnumerator ReducePoisonStack(int duration)
    {
        yield return new WaitForSeconds(duration);
        poisonStack--;
        Debug.Log("Remaining poison - " + poisonStack);
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
