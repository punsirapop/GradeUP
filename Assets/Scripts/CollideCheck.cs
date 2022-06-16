using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject player;
    SpriteRenderer sr;
    int playerClass;
    float angle, knockSpeed;
    List<Color> colorList = new List<Color>();
    void Start()
    {
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

            // Get color
            sr = GetComponent<SpriteRenderer>();
        // Set colors
        colorList.Add(Color.red);
        colorList.Add(Color.yellow);
        colorList.Add(Color.blue);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // On hit - compare color of attack
        if (collision.CompareTag("Hit"))
        {
            Debug.Log("Hit!");
            List<Color> colors = new List<Color>();
            Color newColor = collision.GetComponent<SpriteRenderer>().color;

            // If attack is from Art
            if(playerClass == 0 && colorList.Contains(newColor))
            {
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
            }
            // If knockback - calculate angle and set initial speed
            else if(playerClass == 2)
            {
                knockSpeed = .1f;
                StartCoroutine(Knockback(collision));
            }
        }
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
}
