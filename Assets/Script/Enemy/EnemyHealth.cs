using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : HealthSystem
{
    public enemySO enemyStat;

    StatusManager playerStatusManager;
    SpriteRenderer spriteRenderer;
    Collider2D thisCollider;
    Rigidbody2D thisRB;

    bool isBurnt = false, isBurning = false;

    readonly object burnLock = new object();

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerStatusManager = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
        thisCollider = GetComponent<Collider2D>();
        thisRB = GetComponent<Rigidbody2D>();
        max_HP = enemyStat.HP;
        Current_HP = max_HP;

        ClassLng.ScreenHit += ScreenHit;
    }

    void OnDisable()
    {
        ClassLng.ScreenHit -= ScreenHit;
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
                Debug.Log("Enemy got Hit!");
                GetDamage(playerStatusManager.Atk);
                PrintHP();
                break;
            case "Knock":
                Debug.Log("Enemy got Knocked!");
                GetDamage(playerStatusManager.Atk);
                KnockBack(collision);
                PrintHP();
                break;
            case "PlayerPoison":
                Debug.Log("Enemy got Poison!");
                StartCoroutine(GetOvertimeDamage(playerStatusManager.Atk / 4, 4));
                break;
            case "PlayerFire":
                Debug.Log("Enemy got Burnt!");
                isBurnt = true;
                break;
            case "Art":
                List<Color> colors = new List<Color>();
                Color newColor = collision.GetComponent<SpriteRenderer>().color;
                if (playerStatusManager.ActiveSubClass == 1)
                {
                    colors.Add(newColor);
                    colors.Add(newColor);
                }
                else
                {
                    colors.Add(spriteRenderer.color);
                    colors.Add(newColor);
                }
                MixColor(colors);
                break;
        }
    }

    void KnockBack(Collider2D collision)
    {
        Debug.Log("Start knocking back");
        Vector2 distance = (transform.position - collision.transform.position).normalized;
        Debug.Log(distance);
        thisRB.AddForce(distance * 5f, ForceMode2D.Impulse);
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
        PrintHP();
        yield return new WaitForSeconds(1);
        isBurning = false;
    }

    void MixColor(List<Color> colors)
    {
        // Mix same color
        if (colors[0].Equals(colors[1]))
        {
            if (colors.Contains(Color.red))
            {
                Debug.Log("WED");
            }
            else if (colors.Contains(Color.yellow))
            {
                Debug.Log("YELLO");
            }
            else if (colors.Contains(Color.blue))
            {
                Debug.Log("BLU");
            }
        }
        // Mix different color
        else
        {
            if (colors.Contains(Color.red) && colors.Contains(Color.yellow))
            {
                Debug.Log("ORANG");
            }
            else if (colors.Contains(Color.red) && colors.Contains(Color.blue))
            {
                Debug.Log("PUPEL");
            }
            else if (colors.Contains(Color.yellow) && colors.Contains(Color.blue))
            {
                Debug.Log("GWEEN");
            }
            // Mix failed
            else
            {
                spriteRenderer.color = colors[1];
            }
        }
        // Reset color if mix success
        if (!colors[0].Equals(Color.white))
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void ScreenHit()
    {
        if (gameObject.GetComponentInChildren<SpriteRenderer>().isVisible)
        {
            Debug.Log("Get Attacked by ScreenATK");
            GetDamage(playerStatusManager.Atk);
            PrintHP();
        }
    }
}
