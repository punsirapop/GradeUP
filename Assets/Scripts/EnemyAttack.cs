using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject player;
    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Rotate enemy
        Vector3 rotationVector = player.transform.position - transform.position;
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .05f);

        // Check if hit something
        RaycastHit2D[] hits = Physics2D.RaycastAll(lr.transform.position, transform.rotation * new Vector2(0f, 1000f));
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.up * 1000f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                lr.SetPosition(1, Vector3.up * hit.distance);
            }
        }
    }
}
