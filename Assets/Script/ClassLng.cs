using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLng : characterCon
{
    [SerializeField] public static int ActiveSubClass = 2;
    [SerializeField] GameObject hitWave , hitBullet;
    bool isAttacking = false, isShooting = false;
    readonly object attackLock = new object();
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lock (attackLock)
        {
            isAttacking = true;
            //_firepoint.position = transform.position + transform.rotation * new Vector3(0f, .5f);
            switch (ActiveSubClass)
            {
                case 0:
                    StartCoroutine(Attack0());
                    break;
                case 1:
                    StartCoroutine(Attack0());
                    break;
                case 2:
                    StartCoroutine(Attack2());
                    break;
                case 3:
                    //StartCoroutine(Attack3());
                    break;
            }
            Debug.Log("Attacked");
        }
    }
    IEnumerator Attack0()
    {
        isShooting = true;
        GameObject hitBox = Instantiate(hitWave, _firepoint.position,
            Quaternion.AngleAxis(90f, Vector3.forward) * _firepoint.rotation, transform);
        float punchTime = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(punchTime);
        isShooting = false;
        isAttacking = false;
        yield break;
    }
    IEnumerator Attack2()
    {
        List<GameObject> hitboxes = new List<GameObject>();

        isShooting = true;
        for (int i = 0; i < 6; i++)
        {
            Vector3 position = Quaternion.AngleAxis(i * 360 / 6, Vector3.forward) * _firepoint.position;
            Quaternion rotation = Quaternion.AngleAxis(i * 360 / 6, Vector3.forward);
            GameObject hitBox = Instantiate(hitBullet, position, rotation, _firepoint.transform);
            hitboxes.Add(hitBox);
        }

        // Swing
        float swingAngle = 0f;
        float swingTime = 5 / Atk_Speed;
        for (float time = 0; time < swingTime; time += Time.deltaTime)
        {
            foreach (GameObject hitBox in hitboxes)
            {
                hitBox.transform.position = transform.position + hitBox.transform.rotation *
                    Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector2(3f, 0f);
                swingAngle += 30 * Time.deltaTime / swingTime;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        foreach (GameObject hitBox in hitboxes)
        {
            Destroy(hitBox);
        }

        isShooting = false;
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
                break;

            case 1: //default++
                ActiveSubClass = 1;
                break;

            case 2: //spin
                ActiveSubClass = 2;
                break;

            case 3: //all screen
                ActiveSubClass = 3;
                break;

            default:
                break;
        }
    }

}
