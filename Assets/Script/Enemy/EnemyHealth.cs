using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyHealth : HealthSystem
{
    public enemySO enemyStat;

    StatusManager playerStatusManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] NavMeshAgent agent;

    bool isBurnt = false, isBurning = false;

    int myColor = -1;

    readonly object burnLock = new object();

    void Start()
    {
        playerStatusManager = MainGame.instance.playerController.GetComponent<StatusManager>();
        //playerStatusManager = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
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
                StartCoroutine(KnockBack(collision));
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
                List<int> colors = new List<int>();
                int newColor = ClassArt.PlayerColor;
                if (playerStatusManager.ActiveSubClass == 1)
                {
                    colors.Add(newColor);
                    colors.Add(newColor);
                }
                else
                {
                    colors.Add(myColor);
                    colors.Add(newColor);
                }
                MixColor(colors);
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

    IEnumerator KnockBack(Collider2D collision)
    {
        Debug.Log("Start knocking back");
        float speed = agent.speed;
        agent.speed = 0;
        rigidBody.velocity = Vector2.zero;
        Vector2 distance = (transform.position - collision.transform.position).normalized;
        Debug.Log(distance);
        rigidBody.AddForce(distance * 10f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.25f);
        rigidBody.velocity = Vector2.zero;
        agent.speed = speed;
        yield break;
    }

    IEnumerator Burning()
    {
        isBurning = true;
        GetDamage(playerStatusManager.Atk / 4);
        PrintHP();
        yield return new WaitForSeconds(1);
        isBurning = false;
    }

    void MixColor(List<int> colors)
    {
        // Mix same color
        if (colors[0].Equals(colors[1]))
        {
            if (colors.Contains(0))
            {
                Debug.Log("WED");
            }
            else if (colors.Contains(1))
            {
                Debug.Log("YELLO");
            }
            else if (colors.Contains(2))
            {
                Debug.Log("BLU");
            }
        }
        // Mix different color
        else
        {
            if (colors.Contains(0) && colors.Contains(1))
            {
                Debug.Log("ORANG");
            }
            else if (colors.Contains(0) && colors.Contains(2))
            {
                Debug.Log("PUPEL");
            }
            else if (colors.Contains(1) && colors.Contains(2))
            {
                Debug.Log("GWEEN");
            }
            // Mix failed
            else
            {
                myColor = colors[1];
            }
        }
        // Reset color if mix success
        if (!colors[0].Equals(-1))
        {
            myColor = -1;
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
