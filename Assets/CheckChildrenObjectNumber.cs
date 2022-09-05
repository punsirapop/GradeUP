using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckChildrenObjectNumber : MonoBehaviour
{
    public event Action OnRoomCleared;

    void Update()
    {
        int childCount = transform.childCount;
        // Debug.Log("ChildCount = " + childCount);
        if (childCount == 0)
        {
            OnRoomCleared?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
