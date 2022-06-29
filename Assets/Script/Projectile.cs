using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject areaDamage;
    [SerializeField] private GameObject Hitarea;
    [SerializeField] private GameObject Target;
    private void OnEnable()
    {
        FindObjectOfType<classChem>().Setbullet += SetBullet;
    }
    private void SetBullet(Vector3 position)
    {
        FindObjectOfType<classChem>().Setbullet -= SetBullet;
        Target = Instantiate(this.Hitarea, position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Target)
        {
            Instantiate(areaDamage, this.gameObject.transform.position, Quaternion.identity);
            //Instantiate(areaDamage, this.gameObject.transform.position + new Vector3(-1.5f, 0, 0), Quaternion.identity);
            //Instantiate(areaDamage, this.gameObject.transform.position + new Vector3(1.5f, 0, 0), Quaternion.identity);
            Destroy(this.Target);
            Destroy(this.gameObject);
        }

    }
}
