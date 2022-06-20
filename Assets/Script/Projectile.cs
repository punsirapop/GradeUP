using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject areaDamage;

    void awake()
    {
        StartCoroutine(BulletDestroy());
    }
    private IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(areaDamage, this.gameObject.transform.position, Quaternion.identity);
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
