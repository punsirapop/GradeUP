using System.Collections;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] PhysicsMaterial2D bounceMat;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (bounceMat != null)
        {
            rb.sharedMaterial = bounceMat;
        }
        StartCoroutine(BulletDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (bounceMat == null)
        {
            if (collider.gameObject.CompareTag("Wall"))
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