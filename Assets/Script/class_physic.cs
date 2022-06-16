using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class class_physic : characterCon
{
    [SerializeField]private int ActiveSubClass = 0; 
    void Update()
    {
        UpdatePosition();
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnBullet();
        }
    }

    public void SpawnBullet()
    {
        GameObject bull = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(_firepoint.up * _Bulletforce, ForceMode2D.Impulse);

        if(ActiveSubClass == 1)
        {
        GameObject bullleft = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rbleft = bullleft.GetComponent<Rigidbody2D>();
        rbleft.AddForce((Quaternion.Euler(0, 0, 15.5f) * _firepoint.up) * _Bulletforce, ForceMode2D.Impulse);

        GameObject bullright = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rbright = bullright.GetComponent<Rigidbody2D>();
        rbright.AddForce((Quaternion.Euler(0, 0, -15.5f) * _firepoint.up) * _Bulletforce, ForceMode2D.Impulse);
        }
    }

    private void ChangeSubClass(int ID)
    {
        switch (ID)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                break;
            
            case 3:
                break;
            
            default:
                break;
        }
    }
}
