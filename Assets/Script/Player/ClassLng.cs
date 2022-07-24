using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassLng : characterCon
{
    [SerializeField] GameObject hitWave , hitBullet;
    bool isAttacking = false, isShooting = false;
    readonly object attackLock = new object();

    public static event Action ScreenHit;

    private void OnEnable()
    {
        InitializeStats();
        // FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
    }
    protected override void Update()
    {
        base.Update();
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
            //fireRange.position = transform.position + transform.rotation * new Vector3(0f, .5f);
            switch (ActiveSubClass)
            {
                case 0:
                    NormalAttack();
                    break;
                case 1:
                    NormalAttack();
                    break;
                case 2:
                    StartCoroutine(OrbitAttack());
                    break;
                case 3:
                    ScreenAttack();
                    break;
            }
            Debug.Log("Attacked");
        }
    }

    private void NormalAttack() //normal atk
    {
        isShooting = true;
        GameObject bull = Instantiate(hitWave, fireRange.position,
            Quaternion.AngleAxis(90f, Vector3.forward) * fireRange.rotation, fireRange.transform);
        StartCoroutine(OnCooldown());
    }

    IEnumerator OnCooldown()
    {
        float Cooldown = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(Cooldown);
        isShooting = false;
        isAttacking = false;
    }

    IEnumerator OrbitAttack()
    {
        List<GameObject> hitboxes = new List<GameObject>();

        isShooting = true;
        for (int i = 0; i < 6; i++)
        {
            Vector3 position = Quaternion.AngleAxis(i * 360 / 6, Vector3.forward) * fireRange.position;
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
            yield return null;
        }

        foreach (GameObject hitBox in hitboxes)
        {
            Destroy(hitBox);
        }

        isShooting = false;
        isAttacking = false;
        yield break;
    }
    void ScreenAttack()
    {
        ScreenHit?.Invoke();
        StartCoroutine(OnCooldown());
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
}
