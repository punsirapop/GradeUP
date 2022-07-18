using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform playerpoint;
    private float radius = 0.7f;
    [SerializeField] private LayerMask detectlayer;
    void Update()
    {
        
        if (DetectObject())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("INTERACT YAY");
                //Check tag add use Onuse()
            }
        }
    }
    private bool DetectObject()
    {
        bool isDetected = Physics2D.OverlapCircle(playerpoint.position,radius,detectlayer);
        return isDetected;
    }
    /*private void OnDrawGizmos()
    {
    }*/
}
