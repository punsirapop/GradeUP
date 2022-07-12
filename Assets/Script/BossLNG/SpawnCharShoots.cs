using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharShoots : MonoBehaviour
{
    float turnSpeed;

    public void Setup(Vector2 position, float turnSpeed)
    {
        transform.position = position;
        this.turnSpeed = turnSpeed;
    }

    void Update()
    {
        if (turnSpeed != 0)
        {
            RotateObject();
        }
    }

    private void RotateObject()
    {
        Debug.Log(transform.rotation);
        transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed);
    }
}
