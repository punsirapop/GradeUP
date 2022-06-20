//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour
//{
//    public int subclass;
//    [SerializeField]
//    float aspd;
//    [SerializeField]
//    GameObject hitRange, hitPunch, hitSwing, bullet;
//    GameObject hitBox;
//    Rigidbody2D rb;
//    Quaternion rotation;
//    float x, y;
//    bool clickL = false, clickR = false, attacking = false, charging = false;
//    readonly object clickLock = new object();

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        // Get movement
//        x = Input.GetAxis("Horizontal");
//        y = Input.GetAxis("Vertical");

//        // Check attack type
//        if (!attacking && Input.GetMouseButton(0))
//        {
//            clickL = true;
//        }
//        if (!attacking && Input.GetMouseButton(1))
//        {
//            clickR = true;
//        }

//        // Rotate player on cursor while charging or not attacking
//        if (!attacking || charging)
//        {
//            Vector2 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
//            float angle = Mathf.Atan2(cursor.y, cursor.x) * Mathf.Rad2Deg;
//            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//            transform.rotation = rotation;
//        }

//        // punch
//        if (clickL && !attacking)
//        {
//            lock (clickLock)
//            {
//                attacking = true;
//                switch (subclass)
//                {
//                    case 0:
//                        StartCoroutine(Punch0());
//                        break;
//                    case 1:
//                        StartCoroutine(Punch1());
//                        break;
//                    case 2:
//                        StartCoroutine(Punch2());
//                        break;
//                    case 3:
//                        StartCoroutine(Punch3());
//                        break;
//                }
//                Debug.Log("Punched");
                
//            }
//        }

//        // swing
//        if (clickR && !attacking)
//        {
//            lock (clickLock)
//            {
//                attacking = true;
//                switch (subclass)
//                {
//                    case 0:
//                        StartCoroutine(Swing0());
//                        break;
//                    case 1:
//                        StartCoroutine(Swing1());
//                        break;
//                    case 2:
//                        StartCoroutine(Swing2());
//                        break;
//                    case 3:
//                        StartCoroutine(Swing3());
//                        break;
//                }
//                Debug.Log("Swung");
//            }
//        }
//    }

//    void FixedUpdate()
//    {
//        // Move player while not attacking
//        if (!attacking)
//        {
//            rb.velocity = new Vector2(x, y) * Time.deltaTime * 500f;
//            hitRange.transform.position = transform.position + rotation * new Vector3(1.5f, 0f);
//        }
//        else
//        {
//            rb.velocity = Vector2.zero;
//        }
//    }

//    // normal attack
//    IEnumerator Punch0()
//    {
//        if (attacking)
//        {
//            // Generate hitbox
//            hitBox = Instantiate(hitPunch, hitRange.transform);
//            // Forced stop moving
//            rb.velocity = Vector2.zero;
//            // Set reload time
//            float punchTime = 5 / (2 * aspd);
//            yield return new WaitForSeconds(punchTime);
//            Destroy(hitBox);
//            yield return new WaitForSeconds(punchTime);
//            clickL = false;
//            attacking = false;
//        }
//    }

//    // charged punch
//    IEnumerator Punch1()
//    {
//        if (attacking)
//        {
//            // Initiate charging sequence
//            charging = true;
//            // Generate charge destination indicator
//            GameObject hitMaxRange = Instantiate(hitSwing, hitRange.transform);
//            hitMaxRange.tag = "Untagged";
//            while (Input.GetMouseButton(0))
//            {
//                // Keep extending indicator until set position
//                if (Vector2.Distance(transform.position, hitMaxRange.transform.position) < 5f)
//                {
//                    hitMaxRange.transform.position += rotation * new Vector2(.05f, 0f);
//                }
//                yield return new WaitForFixedUpdate();
//            }
//            hitMaxRange.transform.SetParent(null);
//            // End charging sequence
//            charging = false;
//            // Set dash speed
//            float spd = 5f;
//            // Generate hitbox
//            hitBox = Instantiate(hitSwing, hitRange.transform);
//            // Dash player until reaching destination
//            while (Vector2.Distance(transform.position, hitMaxRange.transform.position) > .05f)
//            {
//                transform.position = Vector2.Lerp(transform.position, hitMaxRange.transform.position, Time.deltaTime * spd);
//                yield return new WaitForFixedUpdate();
//            }
//            Destroy(hitMaxRange);
//            Destroy(hitBox);
//            // Set reload time
//            float punchTime = 5 / (2 * aspd);
//            yield return new WaitForSeconds(punchTime);
//            clickL = false;
//            attacking = false;
//        }
//    }

//    // speed punch
//    IEnumerator Punch2()
//    {
//        if (attacking)
//        {
//            hitBox = Instantiate(hitPunch, hitRange.transform);
//            rb.velocity = Vector2.zero;
//            float punchTime = 5 / (2 * aspd * 3);
//            yield return new WaitForSeconds(punchTime);
//            Destroy(hitBox);
//            yield return new WaitForSeconds(punchTime);
//            clickL = false;
//            attacking = false;
//        }
//    }

//    // baseball swing + knockback
//    IEnumerator Punch3()
//    {
//        if (attacking)
//        {
//            hitBox = Instantiate(hitSwing, hitRange.transform);
//            rb.velocity = Vector2.zero;
//            // Set swing angle, start range, and time
//            float swingAngle = -90f, swingRange = 180, swingTime = 5 / (2 * aspd);
//            // Move hitbox in circular motion within swing time limit
//            for (float time = 0; time < swingTime; time += Time.deltaTime)
//            {
//                hitBox.transform.position = transform.position + rotation * Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector3(2f, 0f);
//                swingAngle += swingRange * Time.deltaTime / swingTime;
//                yield return new WaitForSeconds(Time.deltaTime);
//            }
//            Destroy(hitBox);
//            yield return new WaitForSeconds(swingTime);
//            clickL = false;
//            attacking = false;
//        }
//    }

//    // normal attack
//    IEnumerator Swing0()
//    {
//        if (attacking)
//        {
//            hitBox = Instantiate(hitSwing, hitRange.transform);
//            // Randomize color of attack
//            int color = Random.Range(0, 3);
//            switch (color)
//            {
//                case 0:
//                    hitBox.GetComponent<SpriteRenderer>().color = Color.red;
//                    break;
//                case 1:
//                    hitBox.GetComponent<SpriteRenderer>().color = Color.yellow;
//                    break;
//                case 2:
//                    hitBox.GetComponent<SpriteRenderer>().color = Color.blue;
//                    break;
//            }
//            rb.velocity = Vector2.zero;
//            // Swing
//            float swingAngle = -90f;
//            float swingTime = 5 / (2 * aspd);
//            for (float time = 0; time < swingTime; time += Time.deltaTime)
//            {
//                hitBox.transform.position = transform.position + rotation * Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector3(2f, 0f);
//                swingAngle += 180 * Time.deltaTime / swingTime;
//                yield return new WaitForSeconds(Time.deltaTime);
//            }
//            Destroy(hitBox);
//            yield return new WaitForSeconds(swingTime);
//            clickR = false;
//            attacking = false;
//        }
//    }

//    // Swing but not proc color
//    IEnumerator Swing1()
//    {
//        StartCoroutine(Swing0());
//        yield break;
//    }
    
//    // Paintball gun
//    IEnumerator Swing2()
//    {
//        charging = true;
//        hitBox = Instantiate(bullet, hitRange.transform.position, transform.rotation, hitRange.transform);
//        // Randomize color of attack
//        int color = Random.Range(0, 3);
//        switch (color)
//        {
//            case 0:
//                hitBox.GetComponent<SpriteRenderer>().color = Color.red;
//                break;
//            case 1:
//                hitBox.GetComponent<SpriteRenderer>().color = Color.yellow;
//                break;
//            case 2:
//                hitBox.GetComponent<SpriteRenderer>().color = Color.blue;
//                break;
//        }
//        float punchTime = 5 / (2 * aspd);
//        yield return new WaitForSeconds(punchTime);
//        charging = false;
//        clickR = false;
//        attacking = false;
//        yield break;
//    }
    
//    // Dash through enemy
//    IEnumerator Swing3()
//    {
//        if (attacking)
//        {
//            GameObject hitMaxRange = Instantiate(hitSwing, hitRange.transform);
//            hitMaxRange.tag = "Untagged";
//            hitMaxRange.transform.position += rotation * new Vector2(3f, 0f);
//            hitMaxRange.transform.SetParent(null);
//            Destroy(hitMaxRange.GetComponent<SpriteRenderer>());
//            float spd = 5f;
//            hitBox = Instantiate(hitSwing, hitRange.transform);
//            int color = Random.Range(0, 3);
//            switch (color)
//            {
//                case 0:
//                    hitBox.GetComponent<SpriteRenderer>().color = Color.red;
//                    break;
//                case 1:
//                    hitBox.GetComponent<SpriteRenderer>().color = Color.yellow;
//                    break;
//                case 2:
//                    hitBox.GetComponent<SpriteRenderer>().color = Color.blue;
//                    break;
//            }
//            while (Vector2.Distance(transform.position, hitMaxRange.transform.position) > .05f)
//            {
//                transform.position = Vector2.Lerp(transform.position, hitMaxRange.transform.position, Time.deltaTime * spd);
//                yield return new WaitForFixedUpdate();
//            }
//            Destroy(hitMaxRange);
//            Destroy(hitBox);
//            rb.velocity = Vector2.zero;
//            float punchTime = 5 / (2 * aspd);
//            yield return new WaitForSeconds(punchTime);
//            clickR = false;
//            attacking = false;
//        }
//    }
//}
