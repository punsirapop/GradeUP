using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    StatusManager playerStatusManager;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //angle = Mathf.Atan2(player.transform.position.y - transform.position.y,player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        //playerStatusManager = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
    }

    void FixedUpdate()
    {
        float scale = 1.03f;
        float i = 0f;
        if(MainGame.instance.playerController.ActiveSubClass == 1)
        {
            i = 0.01f;
        }
        // transform.position += Quaternion.AngleAxis(0f, Vector3.forward) * new Vector2(0f, 0f);
        // transform.position += new Vector3(0f, 0f);
        transform.localScale *= new Vector2(scale + i, scale + i);
    }
    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DelayDestroy()
    {
        float delay = .5f;
        if(playerStatusManager.ActiveSubClass == 1)
        {
            delay = .5f;
        }
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    */
}
