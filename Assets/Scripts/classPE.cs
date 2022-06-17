using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classPE : PlayerController
{
    public static int subclass = 0;
    public float aspd = 10f;

    [SerializeField] GameObject hitPunch, hitSwing;

    bool isAttacking = false, isCharging = false;

    readonly object attackLock = new object();

    void Update()
    {
        if (!isAttacking)
        {
            UpdatePosition();
        }
        else
        {
            if (isCharging)
            {
                _mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            }
            _movement = Vector2.zero;
        }

        if (!isAttacking && Input.GetMouseButton(0))
        {
            lock (attackLock)
            {
                isAttacking = true;
                switch (subclass)
                {
                    case 0:
                        StartCoroutine(Attack0());
                        break;
                    case 1:
                        StartCoroutine(Attack1());
                        break;
                    case 2:
                        StartCoroutine(Attack2());
                        break;
                    case 3:
                        StartCoroutine(Attack3());
                        break;
                }
                Debug.Log("Attacked");
            }
        }
    }

    // normal attack
    IEnumerator Attack0()
    {
        if (isAttacking)
        {
            // Generate hitbox
            GameObject hitBox = Instantiate(hitPunch, _firepoint.position, _firepoint.rotation, firepoint.transform);
            // Forced stop moving
            rb.velocity = Vector2.zero;
            // Set reload time
            float punchTime = 5 / (2 * aspd);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            isAttacking = false;
        }
    }

    // charged punch
    IEnumerator Attack1()
    {
        if (isAttacking)
        {
            // Initiate charging sequence
            isCharging = true;
            // Generate charge destination indicator
            GameObject hitMaxRange = Instantiate(hitSwing, _firepoint.position, _firepoint.rotation, firepoint.transform);
            hitMaxRange.tag = "Untagged";
            while (Input.GetMouseButton(0))
            {
                // Keep extending indicator until set position
                if (Vector2.Distance(transform.position, hitMaxRange.transform.position) < 5f)
                {
                    hitMaxRange.transform.position += _firepoint.rotation * new Vector2(0f, .05f);
                }
                yield return new WaitForFixedUpdate();
            }
            hitMaxRange.transform.SetParent(null);
            // End charging sequence
            isCharging = false;
            // Set dash speed
            float spd = 5f;
            // Generate hitbox
            GameObject hitBox = Instantiate(hitPunch, _firepoint.position,
                Quaternion.AngleAxis(90f, Vector3.forward) * _firepoint.rotation, firepoint.transform);
            // Dash player until reaching destination
            while (Vector2.Distance(transform.position, hitMaxRange.transform.position) > .05f)
            {
                transform.position = Vector2.Lerp(transform.position, hitMaxRange.transform.position, Time.deltaTime * spd);
                yield return new WaitForFixedUpdate();
            }
            Destroy(hitMaxRange);
            Destroy(hitBox);
            // Set reload time
            float punchTime = 5 / (2 * aspd);
            yield return new WaitForSeconds(punchTime);
            isAttacking = false;
        }
    }

    // speed punch
    IEnumerator Attack2()
    {
        if (isAttacking)
        {
            GameObject hitBox = Instantiate(hitPunch, _firepoint.position, _firepoint.rotation, firepoint.transform);
            rb.velocity = Vector2.zero;
            float punchTime = 5 / (2 * aspd * 3);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            isAttacking = false;
        }
    }

    // baseball swing + knockback
    IEnumerator Attack3()
    {
        if (isAttacking)
        {
            GameObject hitBox = Instantiate(hitPunch, _firepoint.position, _firepoint.rotation, firepoint.transform);
            rb.velocity = Vector2.zero;
            // Set swing angle, start range, and time
            float swingAngle = -90f, swingRange = 180, swingTime = 5 / (2 * aspd);
            // Move hitbox in circular motion within swing time limit
            for (float time = 0; time < swingTime; time += Time.deltaTime)
            {
                hitBox.transform.position = transform.position + _firepoint.rotation *
                    Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector3(0f, 2f);
                hitBox.transform.rotation = _firepoint.rotation * Quaternion.AngleAxis(swingAngle, Vector3.forward);
                swingAngle += swingRange * Time.deltaTime / swingTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            Destroy(hitBox);
            yield return new WaitForSeconds(swingTime);
            isAttacking = false;
        }
    }
}
