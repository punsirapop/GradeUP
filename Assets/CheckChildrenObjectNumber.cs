using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChildrenObjectNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAllEnemyDead()
    {
        int childCount = transform.childCount;
        Debug.Log("ChildCount = " + childCount);
        if (childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
