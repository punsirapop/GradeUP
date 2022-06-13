using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public int bullet_mode;

    [SerializeField] PhysicsMaterial2D bounceMat;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(bullet_mode == 0)
        {

        }
        if (bullet_mode == 1)
        {
            rb.sharedMaterial = bounceMat;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bullet_mode == 0)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
