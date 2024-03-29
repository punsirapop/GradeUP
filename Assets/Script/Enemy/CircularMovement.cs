using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField]
    private float angularSpeed = 2f;

    private float angle = 0f;
        
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        angle += Time.deltaTime * angularSpeed * 10;

        if(angle >= 360f)
        {
            angle = 0f;
        }
    }
}
