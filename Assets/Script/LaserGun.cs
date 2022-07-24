using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//*************************************
public class LaserGun : AttackHandler
//*************************************
{
    public static event Action<GameObject> OnLaserHit;

    GameObject player;
    LineRenderer lr;
    float playerDistance;
    float wallDistance;
    public float rotateSpeed = 5;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        //Debug.Log("lr = " + lr);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        // Rotate enemy
        Vector3 rotationVector = player.transform.position - transform.position;
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.1f * rotateSpeed);

        // Check if hit something
        RaycastHit2D[] hits = Physics2D.RaycastAll(lr.transform.position, transform.rotation * new Vector2(0f, 1000f));
        //Debug.Log(hits.Length);
        lr.SetPosition(0, new Vector3(0f, 0f, -1f));
        lr.SetPosition(1, new Vector3(0, 100, -1f));

        Debug.Log("Shooting laser");
        if (hits[1].collider.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            playerDistance = hits[1].distance;
            // lr.SetPosition(1, Vector3.up * playerDistance);
            //***********************************************************************
            if (string.Equals(name, "LaserGun(Clone)") && !CharacterCon.isIFramed)
            {
                OnLaserHit?.Invoke(gameObject);
            }
            //***********************************************************************
        }
        else if (hits[1].collider.CompareTag("Wall"))
        {
            wallDistance = hits[1].distance;
            // lr.SetPosition(1, Vector3.up * wallDistance);
        }        
    }
}