using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
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
        float scale = 1.05f;
        if(classLng.subclass == 1)
        {
            scale = 1.06f;
        }
        transform.position += Quaternion.AngleAxis(angle, Vector3.forward) * new Vector2(-.1f, 0f);
        transform.localScale *= new Vector2(scale, scale);
    }
    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    */

    IEnumerator DelayDestroy()
    {
        float delay = .5f;
        if(classLng.subclass == 1)
        {
            delay = .6f;
        }
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
