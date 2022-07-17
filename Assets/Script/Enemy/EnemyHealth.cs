using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : HealthSystem
{
    public enemySO enemyStat;

    StatusManager playerStatusManager;

    bool isBurnt = false, isBurning = false;

    int i = 0;

    readonly object burnLock = new object();

    void Start()
    {
        playerStatusManager = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
        max_HP = enemyStat.HP;
        Current_HP = max_HP;
    }

    void Update()
    {
        if (Current_HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (isBurnt && !isBurning)
        {
            StartCoroutine(Burning());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Got hit with " + collision.tag);
        switch (collision.tag)
        {
            case "Hit":
                Debug.Log("Enemy got hit!");
                GetDamage(playerStatusManager.Atk);
                Debug.Log("HP Left: " + Current_HP + " / " + max_HP);
                break;
            case "PlayerPoison":
                Debug.Log("Enemy got Poison!");
                StartCoroutine(GetOvertimeDamage(playerStatusManager.Atk/4, 4));
                Debug.Log("HP Left: " + Current_HP + " / " + max_HP);
                break;
            case "PlayerFire":
                Debug.Log("Enemy got Burnt!");
                isBurnt = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFire"))
        {
            isBurnt = false;
        }
    }

    IEnumerator Burning()
    {
        isBurning = true;
        GetDamage(playerStatusManager.Atk / 4);
        Debug.Log("HP Left: " + Current_HP + " / " + max_HP);
        yield return new WaitForSeconds(1);
        isBurning = false;
    }
}
