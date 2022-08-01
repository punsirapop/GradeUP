using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotate : MonoBehaviour
{
    LineRenderer lr;
    float playerDistance;
    float wallDistance;
    public float rotateSpeed = 5;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        //Rotate
        transform.Rotate(0f, 0f, (rotateSpeed * Time.deltaTime));

        // Check if hit something
        RaycastHit2D hit = Physics2D.Raycast(lr.transform.position, transform.rotation * new Vector2(0f, 1000f));
        lr.SetPosition(0, new Vector3(0f, 0f, -1f));
        lr.SetPosition(1, new Vector3(0, 100, -1f));
        Debug.Log("laserrrr");

        if (hit.collider.CompareTag("Player"))
        {
            playerDistance = hit.distance;
            lr.SetPosition(1, Vector3.up * playerDistance);
        }

        else if (hit.collider.CompareTag("Wall"))
        {
            wallDistance = hit.distance;
            lr.SetPosition(1, Vector3.up * wallDistance);

        }
    }
}
