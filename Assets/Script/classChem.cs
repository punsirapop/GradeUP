using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classChem : characterCon
{
    void Update()
    {
        UpdatePosition();
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet() //normal atk
    {

        GameObject bull = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(_firepoint.up * _Bulletforce, ForceMode2D.Impulse);
    }
    private void ChangeSubClass(int ID) //for Change Sub-Class 
    {
        switch (ID)
        {
            case 0: //default
                break;

            case 1: //Explotion
                break;

            case 2: //Posion
                break;

            case 3: //Burn
                break;

            default:
                break;
        }
    }
}
