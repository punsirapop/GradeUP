using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classArt : PlayerController
{
    public static int subclass = 3;
    public float aspd = 10f;

    [SerializeField] GameObject bullets, hitSwing;

    bool isAttacking = false, isShooting = false;

    readonly object attackLock = new object();

    void Update()
    {
        if (!isAttacking || isShooting)
        {
            UpdatePosition();
        }
        else
        {
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

    // normal attack
    IEnumerator Swing0()
    {
        GameObject hitBox = Instantiate(hitSwing, _firepoint.position, _firepoint.rotation, firepoint.transform);
        // Randomize color of attack
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
        // Swing
        float swingAngle = 0f;
        float swingTime = 5 / (2 * aspd);
        for (float time = 0; time < swingTime; time += Time.deltaTime)
        {
            hitBox.transform.position = transform.position + _firepoint.rotation *
                Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector2(2f, 0f);
            swingAngle += 180 * Time.deltaTime / swingTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(hitBox);
        yield return new WaitForSeconds(swingTime);
        isAttacking = false;
        yield break;
    }

    // Swing but not proc color
    IEnumerator Swing1()
    {
        StartCoroutine(Swing0());
        yield break;
    }

    // Paintball gun
    IEnumerator Swing2()
    {
        isShooting = true;
        GameObject hitBox = Instantiate(_bullet, _firepoint.position, _firepoint.rotation, bullets.transform);
        // Randomize color of attack
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
        float punchTime = 5 / (2 * aspd);
        yield return new WaitForSeconds(punchTime);
        isShooting = false;
        isAttacking = false;
        yield break;
    }
    
    // Dash through enemy
    IEnumerator Swing3()
    {
        GameObject hitMaxRange = Instantiate(hitSwing, _firepoint.position, _firepoint.rotation, firepoint.transform);
        hitMaxRange.tag = "Untagged";
        hitMaxRange.transform.position += _firepoint.rotation * new Vector2(0f, 3f);
        hitMaxRange.transform.SetParent(null);
        Destroy(hitMaxRange.GetComponent<SpriteRenderer>());
        float spd = 5f;
        GameObject hitBox = Instantiate(hitSwing, _firepoint.position, _firepoint.rotation, firepoint.transform);
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
        while (Vector2.Distance(transform.position, hitMaxRange.transform.position) > .05f)
        {
            transform.position = Vector2.Lerp(transform.position, hitMaxRange.transform.position, Time.deltaTime * spd);
            yield return new WaitForFixedUpdate();
        }
        Destroy(hitMaxRange);
        Destroy(hitBox);
        rb.velocity = Vector2.zero;
        float punchTime = 5 / (2 * aspd);
        yield return new WaitForSeconds(punchTime);
        isAttacking = false;
        yield break;
    }
}
