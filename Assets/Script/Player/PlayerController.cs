using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 direction = new Vector2(0f, -1f);
    public float currentHP = 100;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentHP += 5;
            Debug.Log("current Hp = " + currentHP);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentHP -= 5;
            Debug.Log("current Hp = " + currentHP);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("current Hp = " + currentHP);
        }

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
    }
}