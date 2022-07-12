using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [SerializeField] enemySO enemyStat;

    StatusManager playerStatusManager;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hit"))
        {
            Debug.Log("Enemy got hit!");
            GetDamage(playerStatusManager.Atk);
            Debug.Log("HP Left: " + Current_HP + " / " + max_HP);
        }
    }
}
