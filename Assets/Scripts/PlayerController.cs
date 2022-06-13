using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int subclass;
    [SerializeField]
    float aspd;
    [SerializeField]
    GameObject hitRange, hitPunch, hitSwing;
    GameObject hitBox;
    Rigidbody2D rb;
    Quaternion rotation;
    float x, y;
    bool clickL = false, clickR = false, attacking = false;
    readonly object clickLock = new object();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // movement
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // check attack type
        if (!attacking && Input.GetMouseButton(0))
        {
            clickL = true;
        }
        else if (!attacking && Input.GetMouseButton(1))
        {
            clickR = true;
        }

        // rotate
        if (!attacking)
        {
            Vector2 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(cursor.y, cursor.x) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }

        // punch
        if (clickL && !attacking)
        {
            lock (clickLock)
            {
                attacking = true;
                switch (subclass)
                {
                    case 0:
                        StartCoroutine(Punch0());
                        break;
                    case 1:
                        StartCoroutine(Punch1());
                        break;
                    case 2:
                        StartCoroutine(Punch2());
                        break;
                    case 3:
                        StartCoroutine(Punch3());
                        break;
                }
                Debug.Log("Punched");
                
            }
        }

        // swing
        if (clickR && !attacking)
        {
            lock (clickLock)
            {
                attacking = true;
                switch (subclass)
                {
                    case 0:
                        StartCoroutine(Swing0());
                        break;
                    case 1:
                        StartCoroutine(Swing1());
                        break;
                    case 2:
                        StartCoroutine(Swing2());
                        break;
                    case 3:
                        StartCoroutine(Swing3());
                        break;
                }
                Debug.Log("Swung");
            }
        }
    }

    void FixedUpdate()
    {
        if (!attacking)
        {
            rb.velocity = new Vector2(x, y) * Time.deltaTime * 500f;
            hitRange.transform.position = transform.position + rotation * new Vector3(1.5f, 0f);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    // normal attack
    IEnumerator Punch0()
    {
        if (attacking)
        {
            hitBox = Instantiate(hitPunch, hitRange.transform);
            rb.velocity = Vector2.zero;
            float punchTime = 5 / (2 * aspd);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            clickL = false;
            attacking = false;
        }
    }

    // ?????????
    IEnumerator Punch1()
    {
        if (attacking)
        {
            hitBox = Instantiate(hitPunch, hitRange.transform);
            rb.velocity = Vector2.zero;
            float punchTime = 5 / (2 * aspd);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            clickL = false;
            attacking = false;
        }
    }

    // ???????
    IEnumerator Punch2()
    {
        if (attacking)
        {
            hitBox = Instantiate(hitPunch, hitRange.transform);
            rb.velocity = Vector2.zero;
            aspd *= 10;
            float punchTime = 5 / (2 * aspd);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            clickL = false;
            attacking = false;
        }
    }

    // ??? + knockback
    IEnumerator Punch3()
    {
        if (attacking)
        {
            hitBox = Instantiate(hitPunch, hitRange.transform);
            rb.velocity = Vector2.zero;
            float punchTime = 5 / (2 * aspd);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            clickL = false;
            attacking = false;
        }
    }

    IEnumerator Swing0()
    {
        if (attacking)
        {
            hitBox = Instantiate(hitSwing, hitRange.transform);
            int color = Random.Range(0, 3);
            switch (color)
            {
                case 0:
                    hitBox.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 1:
                    hitBox.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                case 2:
                    hitBox.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
            }
            rb.velocity = Vector2.zero;
            float swingAngle = -90f;
            float swingTime = 5 / (2 * aspd);
            for (float time = 0; time < swingTime; time += Time.deltaTime)
            {
                hitBox.transform.position = transform.position + rotation * Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector3(2f, 0f);
                swingAngle += 180 * Time.deltaTime / swingTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            Destroy(hitBox);
            yield return new WaitForSeconds(swingTime);
            clickR = false;
            attacking = false;
        }
    }

    IEnumerator Swing1()
    {
        yield return new WaitForSeconds(1);
    }
    IEnumerator Swing2()
    {
        yield return new WaitForSeconds(1);
    }
    IEnumerator Swing3()
    {
        yield return new WaitForSeconds(1);
    }
}
