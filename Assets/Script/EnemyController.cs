using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        // Drop some item/gold here//
        Destroy(this.gameObject);
    }

    public float GetEnemyCurrentHealth()
    {
        return currentHealth;
    }

    public void SetEnemyHealth(float setHealth)
    {
        currentHealth = setHealth;
    }
}
