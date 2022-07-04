using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiderAttack : MonoBehaviour
{
    public Transform center;
    public int reverse;
    public float size = 3;

    private bool changeMovement = true;
    // Start is called before the first frame update
    void Start()
    {
        reverse = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeMovement)
        {
            if (reverse == 1) reverse = -1;
            else reverse = 1;
            changeMovement = false;
            //StartCoroutine(ReverseDirection());
        }
        Vector2 direction = center.position - transform.position;
        if(Mathf.Abs(direction.x) > 2 || Mathf.Abs(direction.y) > 2)
        {
            direction.Normalize();
            transform.Translate(direction * -1 * size * Time.deltaTime);
        }
        transform.Translate(direction * -1 * size * Time.deltaTime);
    }

    IEnumerator ReverseDirection()
    {
        yield return new WaitForSeconds(3f);
        changeMovement = true;
    }
}
