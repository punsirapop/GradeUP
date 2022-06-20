using UnityEngine;

public class classPhysic : characterCon
{
    [SerializeField] private int ActiveSubClass = 0;
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

        if (ActiveSubClass == 1)
        {

            GameObject bullleft = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
            Rigidbody2D rbleft = bullleft.GetComponent<Rigidbody2D>();
            rbleft.AddForce((Quaternion.Euler(0, 0, 15.5f) * _firepoint.up) * _Bulletforce, ForceMode2D.Impulse);

            GameObject bullright = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
            Rigidbody2D rbright = bullright.GetComponent<Rigidbody2D>();
            rbright.AddForce((Quaternion.Euler(0, 0, -15.5f) * _firepoint.up) * _Bulletforce, ForceMode2D.Impulse);
        }

        if (ActiveSubClass == 3)
        {
            //Change Bullet
        }
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

            case 1: //3way
                break;

            case 2: //+atk speed
                break;

            case 3: //bounce bullet
                break;

            default:
                break;
        }
    }
}
