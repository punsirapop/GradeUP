using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour
{
    SpriteRenderer currentColor;
    GameObject player;
    PlayerController subclass;
    float angle, knockSpeed;
    List<Color> colorList = new List<Color>();
    void Start()
    {
        // Get subclass
        player = GameObject.Find("Player");
        //subclass = player.GetComponent<PlayerController>();
        currentColor = GetComponent<SpriteRenderer>();
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
            if(colorList.Contains(newColor))
            {
                if(subclass.subclass == 1)
                {
                    colors.Add(newColor);
                    colors.Add(newColor);
                }
                else
                {
                    colors.Add(currentColor.color);
                    colors.Add(newColor);
                }
                MixColor(colors);
            }
            // If knockback - calculate angle and set initial speed
            else
            {
                angle = Mathf.Atan2(collision.transform.root.position.y - transform.position.y,
                    collision.transform.root.position.x - transform.position.x) * Mathf.Rad2Deg;
                knockSpeed = .75f;
                StartCoroutine(Knockback());
            }
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
                currentColor.color = colors[1];
            }
        }
        // Reset color if mix success
        if (!colors[0].Equals(Color.black))
        {
            currentColor.color = Color.black;
        }
    }

    IEnumerator Knockback()
    {
        // move with negative acceleration
        while (knockSpeed > 0)
        {
            transform.position = Vector2.Lerp(transform.position,
                transform.position + Quaternion.AngleAxis(angle, Vector3.forward) * new Vector2(-1f, 0f), knockSpeed);
            knockSpeed /= 1.3f;
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }
}
