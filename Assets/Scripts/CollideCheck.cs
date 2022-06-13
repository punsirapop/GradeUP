using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour
{
    SpriteRenderer currentColor;
    void Start()
    {
        currentColor = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hit"))
        {
            Debug.Log("Hit!");
            Color newColor = collision.GetComponent<SpriteRenderer>().color;
            if (currentColor.color.Equals(Color.red))
            {
                if (newColor.Equals(Color.red))
                {
                    currentColor.color = Color.black;
                    Debug.Log("RED");
                }
                else if (newColor.Equals(Color.yellow))
                {
                    currentColor.color = Color.black;
                    Debug.Log("ORANG");
                }
                else if (newColor.Equals(Color.blue))
                {
                    currentColor.color = Color.black;
                    Debug.Log("PUPEL");
                }
            }
            else if (currentColor.color.Equals(Color.yellow))
            {
                if (newColor.Equals(Color.red))
                {
                    currentColor.color = Color.black;
                    Debug.Log("ORANG");
                }
                else if (newColor.Equals(Color.yellow))
                {
                    currentColor.color = Color.black;
                    Debug.Log("YELLO");
                }
                else if (newColor.Equals(Color.blue))
                {
                    currentColor.color = Color.black;
                    Debug.Log("GREN");
                }
            }
            else if (currentColor.color.Equals(Color.blue))
            {
                if (newColor.Equals(Color.red))
                {
                    currentColor.color = Color.black;
                    Debug.Log("PUPEL");
                }
                else if (newColor.Equals(Color.yellow))
                {
                    currentColor.color = Color.black;
                    Debug.Log("GREN");
                }
                else if (newColor.Equals(Color.blue))
                {
                    currentColor.color = Color.black;
                    Debug.Log("BLU");
                }
            }
            else
            {
                currentColor.color = newColor;
            }
        }
    }
}
