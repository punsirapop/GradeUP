using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassArt : characterCon
{
    public static int subclass = 0;
    public float aspd = 10f;

    [SerializeField] GameObject bullets, hitSwing, hitPunch;

    bool isAttacking = false, isShooting = false;

    readonly object attackLock = new object();

    void Start()
    {
        
    }

    // Update is called once per frame
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
            switch (subclass)
            {
                case 0:
                    //StartCoroutine(NormalPunch());
                    break;
                case 1:
                    //StartCoroutine(ChargePunch());
                    break;
                case 2:
                    //StartCoroutine(SpeedPunch());
                    break;
                case 3:
                    //StartCoroutine(SwingAtk());
                    break;
            }
            Debug.Log("Punched");

        }
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

            case 1: //
                break;

            case 2: //
                break;

            case 3: //
                break;

            default:
                break;
        }
    }
}
