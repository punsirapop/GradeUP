using System.Collections;
using UnityEngine;

public class classPhysic : characterCon
{
    [SerializeField] private int ActiveSubClass = 0;
    [SerializeField] private GameObject[] _bullet;

    private GameObject _selectBullet;
    private bool isAttacking = false ;

    private void OnEnable()
    {
        InitializeStats();
        // FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
    }
    protected override void Update()
    {
        base.Update();
        UpdatePosition();
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet() //normal atk
    {
        isAttacking = true;
        if (ActiveSubClass == 3)
        {
            _selectBullet = _bullet[1];
        }
        else
        {
            _selectBullet = _bullet[0];
        }

        if (ActiveSubClass == 1)
        {

            GameObject bullleft = Instantiate(_selectBullet, _firepoint.position, _firepoint.rotation);
            Rigidbody2D rbleft = bullleft.GetComponent<Rigidbody2D>();
            rbleft.AddForce((Quaternion.Euler(0, 0, 15.5f) * _firepoint.up) * _Bulletforce, ForceMode2D.Impulse);

            GameObject bullright = Instantiate(_selectBullet, _firepoint.position, _firepoint.rotation);
            Rigidbody2D rbright = bullright.GetComponent<Rigidbody2D>();
            rbright.AddForce((Quaternion.Euler(0, 0, -15.5f) * _firepoint.up) * _Bulletforce, ForceMode2D.Impulse);
        }

        GameObject bull = Instantiate(_selectBullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(_firepoint.up * _Bulletforce, ForceMode2D.Impulse);

        StartCoroutine(OnCooldown());
    }

    IEnumerator OnCooldown()
    {
        float Cooldown = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(Cooldown);
        isAttacking = false;
    }
    private void ChangeSubClass(int ID) //for Change Sub-Class 
    {
        switch (ID)
        {
            case 0: //default
                break;

            case 1: //3way
                ActiveSubClass = 1;
                break;

            case 2: //+atk speed
                ActiveSubClass = 2;
                Atk_Speed += 2;
                break;

            case 3: //bounce bullet
                ActiveSubClass = 3;
                break;

            default:
                break;
        }
        //FindObjectOfType<DebugUI>().ChangeSubClass -= ChangeSubClass; //when debug finish remove plz comment  
    }
}
