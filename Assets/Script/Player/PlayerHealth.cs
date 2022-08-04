using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerHealth : HealthSystem
{
    public static event Action OnPlayerDeath;

    enemySO enemy;
    SpriteRenderer sr;

    [SerializeField] StatusManager statusManager;
    
    [SerializeField] HealthBar healthBar;
    // GameObject statDisplay;
    // [SerializeField] GameObject deadDisplay;

    // TextMeshProUGUI[] Display;

    int poisonStack = 0;

    void Start()
    {
        CharacterCon.OnHit += IsHit;
        CharacterCon.OnPoisoned += IsPoisoned;
        LaserGun.OnLaserHit += IsHit;
        SpawnPlayer.SetCurrentHp += SetHp;

        sr = gameObject.GetComponent<SpriteRenderer>();


        SetMaxHP(statusManager.HP);
        SetHp(PlayerPrefs.GetFloat("playerCurrentHp", max_HP));

        /*
        max_HP = statusManager.HP;
        Current_HP = max_HP;
        */

        Debug.Log("Start HP: " + Current_HP + " / " + max_HP);
        // statDisplay = GameObject.Find("statDisplay");
        // Display = statDisplay.GetComponentsInChildren<TextMeshProUGUI>();
        // deadDisplay = GameObject.Find("DeadScreen");
        // deadDisplay.SetActive(false);

    }

    void OnEnable()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        // statDisplay = GameObject.Find("statDisplay");
        // Display = statDisplay.GetComponentsInChildren<TextMeshProUGUI>();
        //deadDisplay = GameObject.Find("DeadScreen");
    }

    private void OnDisable()
    {
        CharacterCon.OnHit -= IsHit;
        CharacterCon.OnPoisoned -= IsPoisoned;
        LaserGun.OnLaserHit -= IsHit;
        SpawnPlayer.SetCurrentHp -= SetHp;
    }

    void Update()
    {
        UpdateDisplay();

        if (Current_HP <= 0)
        {
            // OnPlayerDeath?.Invoke();
            // if(statDisplay == null)
            //     statDisplay = GameObject.Find("statDisplay");
            // statDisplay.SetActive(false);
            /*
            foreach (TextMeshProUGUI display in Display)
            {
                display.enabled = false;
            }
            */
            // if (deadDisplay == null)
            //     deadDisplay = GameObject.Find("DeadScreen");
            // deadDisplay.SetActive(true);
            // gameObject.SetActive(false);

            // [UI] ADD!
            FindObjectOfType<UIManager>().LoseResultUI();
        }
    }

    void UpdateDisplay()
    {
        /*
        Debug.Log(statDisplay);
        if (statDisplay == null)
        {
            statDisplay = GameObject.Find("statDisplay");
            Display = statDisplay.GetComponentsInChildren<TextMeshProUGUI>();
        }
        */

        // Display[0].text = (Current_HP + " / " + max_HP);
        // Display[1].text = ("ATK: " + statusManager.Atk);
        // Display[2].text = ("SPD: " + statusManager.MoveSpeed);
        // Display[3].text = ("ASPD: " + statusManager.Atk_Speed);

        // [UI] ADD
        if (healthBar == null)
        {
            healthBar = FindObjectOfType<HealthBar>();
            healthBar?.SetMaxHealthBar(max_HP);
        }
        else
        {
            healthBar.SetHealth(Current_HP);
        }
    }

    void IsHit(GameObject enemyAttack)
    {
        try
        {
            enemy = enemyAttack.GetComponent<AttackHandler>().attackOwner;
            Debug.Log("enemy = " + enemy);
        }
        catch (Exception)
        {
            enemy = enemyAttack.GetComponent<EnemyHealth>().enemyStat;
        }

        float damage = enemy.Attack;
        GetDamage(damage);
        Debug.Log("Get attacked by " + enemy.EnemyName + " - " + damage);
        Debug.Log("Remaining HP: " + Current_HP + " / " + max_HP);
        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(IFrame());
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
        Debug.Log("Doing IFrame");
        
        Color oldColor = sr.color;

        sr.color = new Color(1, 0, 0, .5f);
        CharacterCon.isIFramed = true;
        yield return new WaitForSeconds(.5f);

        sr.color = oldColor;
        CharacterCon.isIFramed = false;
        yield break;
    }

    public float GetMaxHp()
    {
        return max_HP;
    }
}
