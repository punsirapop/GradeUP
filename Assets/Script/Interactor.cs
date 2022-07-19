using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform playerpoint;
    private float radius = 0.7f;
    [SerializeField] private LayerMask detectlayer;

    public delegate void OnUseDelegate();
    public OnUseDelegate OnUse;
    void Update()
    {
        
        if (DetectObject())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("INTERACT YAY");
                GetObject().GetComponent<HeelingBox>().UseHealBox();
            }
        }
    }
    private bool DetectObject()
    {
        bool isDetected = Physics2D.OverlapCircle(playerpoint.position,radius,detectlayer);
        return isDetected;
    }

    private GameObject GetObject()
    {
        Collider2D Obj = Physics2D.OverlapCircle(playerpoint.position, radius, detectlayer);
        return Obj.gameObject;
    }
}
