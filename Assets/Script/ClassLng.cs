using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLng : characterCon
{
    [SerializeField] private int ActiveSubClass = 0;
    [SerializeField] GameObject hitWave;
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
                    //StartCoroutine(Attack1());
                    break;
                case 2:
                    //StartCoroutine(Attack2());
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
    protected override void FixedUpdate()
    {
        if (!isAttacking || isShooting)
        {
            GetPosition();
            UpdatePosition();
        }
        else
        {
            //rb.velocity = Vector2.zero;
        }

    }
    private void ChangeSubClass(int ID) //for Change Sub-Class 
    {
        switch (ID)
        {
            case 0: //default
                break;

            case 1: //3way
                ActiveSubClass = 1;
                break;

            case 2: //+atk speed
                ActiveSubClass = 2;
                break;

            case 3: //bounce bullet
                ActiveSubClass = 3;
                break;

            default:
                break;
        }
    }

}
