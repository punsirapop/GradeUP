using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballController : MonoBehaviour
{
    private Vector3 target;
    public float speed;
    private Vector3 direction;
    public bool shoot = false;
    public Rigidbody2D rb;

    private bool isShooted;

    private void Start()
    {
        //target = MainGame.instance.playerController.transform.position;

    }

    private void Update()
    {
        if (shoot)
        {
            ShootFootball();
            shoot = false;
        }
    }

    void ShootFootball()
    {
        //if(Vector2.Distance(target, transform.position) > 0.1f)
        //{
        //    Debug.Log("shooting");
        //    direction = (target - transform.position).normalized;
        //    transform.position += speed * Time.fixedDeltaTime * direction;
        //}
        Debug.Log("shooting");
        //transform.position += speed * Time.deltaTime * direction;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    public void SetIsShoot(bool isShoot)
    {
        shoot = isShoot;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("BossPE"))
        {
            target = MainGame.instance.playerController.transform.position;
            direction = (target - transform.position).normalized;
            SetIsShoot(true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
