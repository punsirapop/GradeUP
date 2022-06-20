using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject player;
    float angle;

    void Start()
    {
        player = GameObject.Find("Player");
        angle = Mathf.Atan2(player.transform.position.y - transform.position.y,
            player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        StartCoroutine(DelayDestroy());
    }

    void FixedUpdate()
    {
        transform.position += Quaternion.AngleAxis(angle, Vector3.forward) * new Vector2(-.25f, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
