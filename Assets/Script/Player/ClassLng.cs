using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLng : characterCon
{
    [SerializeField] public static int ActiveSubClass = 0;
    [SerializeField] GameObject hitWave , hitBullet;
    bool isAttacking = false, isShooting = false;
    readonly object attackLock = new object();

    public delegate void ScreenHitDelegate();
    public ScreenHitDelegate Screenhit;

    private void OnEnable()
    {
        InitializeStats();
        // FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
    }
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
                    StartCoroutine(NormalAttack());
                    break;
                case 1:
                    StartCoroutine(NormalAttack());
                    break;
                case 2:
                    StartCoroutine(OrbitAttack());
                    break;
                case 3:
                    StartCoroutine(ScreenAttack());
                    break;
            }
            Debug.Log("Attacked");
        }
    }


    IEnumerator NormalAttack()
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
    IEnumerator OrbitAttack()
    {
        List<GameObject> hitboxes = new List<GameObject>();

        isShooting = true;
        for (int i = 0; i < 6; i++)
        {
            Vector3 position = Quaternion.AngleAxis(i * 360 / 6, Vector3.forward) * _firepoint.position;
            Quaternion rotation = Quaternion.AngleAxis(i * 360 / 6, Vector3.forward);
            GameObject hitBox = Instantiate(hitBullet, position, rotation);
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
    IEnumerator ScreenAttack()
    {
        Screenhit?.Invoke();
        yield return new WaitForSeconds(0.01f);
        isAttacking = false;
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
        //FindObjectOfType<DebugUI>().ChangeSubClass -= ChangeSubClass; //when debug finish remove plz comment  
    }
}
