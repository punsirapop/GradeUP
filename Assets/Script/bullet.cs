using System.Collections;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] PhysicsMaterial2D bounceMat;

    //for testing
    [SerializeField] private int bullet_mode;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (bullet_mode == 0)
        {

        }
        if (bullet_mode == 1)
        {
            rb.sharedMaterial = bounceMat;
        }
        StartCoroutine(BulletDestroy());
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
    private IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}