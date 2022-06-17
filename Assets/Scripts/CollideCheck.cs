using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour
{
    GameObject player;
    SpriteRenderer sr;
    int playerClass;
    float angle, knockSpeed;

    void Start()
    {
        player = GameObject.Find("Player");
        // Get player's class
        if (player.GetComponent<classArt>().isActiveAndEnabled)
        {
            playerClass = 0;
        }
        else if (player.GetComponent<classLng>().isActiveAndEnabled)
        {
            playerClass = 1;
        }
        else if (player.GetComponent<classPE>().isActiveAndEnabled)
        {
            playerClass = 2;
        }

        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        classLng.class3Attack += LNGClass3Attacked;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // On hit - check class of attack
        if (collision.CompareTag("Hit"))
        {
            List<Color> colors = new List<Color>();
            Color newColor = collision.GetComponent<SpriteRenderer>().color;

            Debug.Log("Hit!");

            switch (playerClass)
            {
                // If attack is from Art - mixin' colors
                case 0:
                    if(classArt.subclass == 1)
                    {
                        colors.Add(newColor);
                        colors.Add(newColor);
                    }
                    else
                    {
                        colors.Add(sr.color);
                        colors.Add(newColor);
                    }
                    MixColor(colors);
                    break;
                // If attack is from Lng - hit
                case 1:
                    break;
                // If attack is from PE - knockback
                case 2:
                    knockSpeed = .1f;
                    StartCoroutine(Knockback(collision));
                    break;
            }
        }
        // If collide with player
        else if (collision.CompareTag("Player"))
        {
            knockSpeed = .1f;
            StartCoroutine(Knockback(collision));
        }
    }

    void MixColor(List<Color> colors)
    {
        // Mix same color
        if (colors[0].Equals(colors[1]))
        {
            if (colors.Contains(Color.red))
            {
                Debug.Log("RED");
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
                sr.color = colors[1];
            }
        }
        // Reset color if mix success
        if (!colors[0].Equals(Color.black))
        {
            sr.color = Color.black;
        }
    }

    IEnumerator Knockback(Collider2D collision)
    {
        angle = Mathf.Atan2(collision.transform.root.position.y - transform.position.y,
            collision.transform.root.position.x - transform.position.x) * Mathf.Rad2Deg;
        // move with negative acceleration
        while (knockSpeed > 0)
        {
            transform.position = Vector2.Lerp(transform.position,
                transform.position + Quaternion.AngleAxis(angle, Vector3.forward) * new Vector2(-1f, 0f), knockSpeed);
            knockSpeed /= 1.05f;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
    
    void LNGClass3Attacked()
    {
        Debug.Log("Called - " + transform.position);
        if (GetComponent<SpriteRenderer>().isVisible)
        {
            Debug.Log("Whole map Hit! - " + transform.position);
        }
    }
}