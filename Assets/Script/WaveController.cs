using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //angle = Mathf.Atan2(player.transform.position.y - transform.position.y,player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        StartCoroutine(DelayDestroy());
    }

    void FixedUpdate()
    {
        float scale = 1.05f;
        if(ClassLng.ActiveSubClass == 1)
        {
            scale = 1.06f;
        }
        transform.position += Quaternion.AngleAxis(0f, Vector3.forward) * new Vector2(0f, 0f);
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
        if(ClassLng.ActiveSubClass == 1)
        {
            delay = .6f;
        }
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
