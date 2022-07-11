using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float  moveSpeed;
    public Vector2 direction = new Vector2(0f, -1f);
    public MoveToNextRoom moveable;
   
    // [BOSSCHEM] Debug for skill2
    public int poisonLevel;
    public int poisonReduceDuration = 3;

    private Coroutine countdownPoison;

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
    }

    // [BOSSCHEM] Debug for skill2
    public void Poisonning()
    {
        poisonLevel++;
        Debug.Log(poisonLevel);
        if (countdownPoison != null)
        {
            StopCoroutine(countdownPoison);
        }
        countdownPoison = StartCoroutine(CountdownReducePoison());
    }
    
    IEnumerator CountdownReducePoison()
    {
        for (int i = 0; i < poisonReduceDuration; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        ResetPoisonLevel();
    }

    public void ResetPoisonLevel()
    {
        Debug.Log("Reset Poison!");
        poisonLevel = 0;
    }
}
