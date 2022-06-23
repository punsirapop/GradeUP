using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassArt : characterCon
{
    
    [SerializeField] GameObject hitSwing, hitPunch;
    [SerializeField] protected GameObject _bullet;
    bool isAttacking = false, isShooting = false;

    readonly object attackLock = new object();

    public static int ActiveSubClass = 0;
    private void OnEnable()
    {
        FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
    }
    private void OnDisable()
    {
        FindObjectOfType<DebugUI>().ChangeSubClass -= ChangeSubClass;
    }
    void Update()
    {
        if (!isAttacking && Input.GetButtonDown("Fire1"))
        {
            Actack();
        }
    }

    private void Actack()
    {
        lock (attackLock)
        {
            isAttacking = true;
            switch (ActiveSubClass)
            {
                case 0:
                    StartCoroutine(NormalAttack());
                    break;
                case 1:
                    StartCoroutine(Attack1());
                    break;
                case 2:
                    StartCoroutine(PaintballGun());
                    break;
                case 3:
                    StartCoroutine(DashAttack());
                    break;
            }
            Debug.Log("Attacked");
        }
    }

    IEnumerator NormalAttack()
    {
        GameObject hitBox = Instantiate(hitSwing, _firepoint.position, _firepoint.rotation, firepoint.transform);
        // Randomize color of attack
        int color = UnityEngine.Random.Range(0, 3);
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
        float swingTime = 5 / (2 * Atk_Speed);
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

    IEnumerator Attack1()
    {
        StartCoroutine(NormalAttack());
        yield break;
    }
    IEnumerator PaintballGun()
    {
        isShooting = true;
        GameObject bull = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(_firepoint.up * _Bulletforce, ForceMode2D.Impulse);
        //GameObject hitBox = Instantiate(_bullet, _firepoint.position, _firepoint.rotation, bullets.transform);
        // Randomize color of attack
        int color = UnityEngine.Random.Range(0, 3);
        switch (color)
        {
            case 0:
                bull.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 1:
                bull.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 2:
                bull.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
        float punchTime = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(punchTime);
        isShooting = false;
        isAttacking = false;
        yield break;
    }
    IEnumerator DashAttack()
    {
        GameObject hitMaxRange = Instantiate(hitSwing, _firepoint.position, _firepoint.rotation, firepoint.transform);
        hitMaxRange.tag = "Untagged";
        hitMaxRange.transform.position += _firepoint.rotation * new Vector2(0f, 3f);
        hitMaxRange.transform.SetParent(null);
        Destroy(hitMaxRange.GetComponent<SpriteRenderer>());
        float spd = 5f;
        GameObject hitBox = Instantiate(hitPunch, _firepoint.position,
            Quaternion.AngleAxis(90f, Vector3.forward) * _firepoint.rotation, firepoint.transform);
        int color = UnityEngine.Random.Range(0, 3);
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
        float punchTime = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(punchTime);
        isAttacking = false;
        yield break;
    }
    protected override void FixedUpdate()
    {
        if (!isAttacking || isShooting)
        {
            GetPosition();
            UpdatePosition();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }
    private void ChangeSubClass(int ID) //for Change Sub-Class 
    {
        switch (ID)
        {
            case 0: //default
                ActiveSubClass = 0;
                break;

            case 1: //Explotion
                ActiveSubClass = 1;
                break;

            case 2: //Posion
                ActiveSubClass = 2;
                break;

            case 3: //Burn
                ActiveSubClass = 3;
                break;

            default:
                break;
        }
    }
}
