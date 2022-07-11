using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;

    bool isVisible;
    Coroutine countDownVisibleCoroutine;

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

    // [BOSSCHEM]
    public void SetVisible(bool visible, float time = 0)
    {
        Debug.Log("Invisible!");
        foreach (Component child in transform)
        {
            SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.enabled = visible;
            }
        }

        if (!visible)
        {
            if (countDownVisibleCoroutine != null)
            {
                StopCoroutine(countDownVisibleCoroutine);
            }
            countDownVisibleCoroutine = StartCoroutine(CountdownToVisible(time));
        }
    }

    IEnumerator CountdownToVisible(float time)
    {
        yield return new WaitForSeconds(time);
        SetVisible(true);
    }
}
