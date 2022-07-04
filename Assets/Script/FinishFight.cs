using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFight : MonoBehaviour
{
    public GameObject moveToRoom;
    void Start()
    {
        moveToRoom.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            moveToRoom.SetActive(true);
        }
    }
}
