using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class classLng : PlayerController
{
    public static int subclass = 2;
    public float aspd = 10f;

    [SerializeField] GameObject bullets, hitWave, hitSwing;
    [SerializeField] UnityEvent attack;

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

        if ((!isAttacking && Input.GetMouseButton(0)))
        {
            lock (attackLock)
            {
                isAttacking = true;
                _firepoint.position = transform.position + transform.rotation * new Vector3(0f, .5f);
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

    IEnumerator Attack0()
    {
        isShooting = true;
        GameObject hitBox = Instantiate(hitWave, _firepoint.position,
            Quaternion.AngleAxis(90f, Vector3.forward) * _firepoint.rotation, transform);
        float punchTime = 5 / (2 * aspd);
        yield return new WaitForSeconds(punchTime);
        isShooting = false;
        isAttacking = false;
        yield break;
    }

    IEnumerator Attack1()
    {
        StartCoroutine(Attack0());
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
            GameObject hitBox = Instantiate(hitSwing, position, rotation, bullets.transform);
            hitboxes.Add(hitBox);
        }

        // Swing
        float swingAngle = 0f;
        float swingTime = 5 / aspd;
        for (float time = 0; time < swingTime; time += Time.deltaTime)
        {
            foreach(GameObject hitBox in hitboxes)
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

    IEnumerator Attack3()
    {
        attack.Invoke();
        yield break;
    }
}
