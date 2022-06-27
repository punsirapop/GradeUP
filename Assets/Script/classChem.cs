using System.Collections;
using UnityEngine;

public class classChem : characterCon
{
    [SerializeField] protected GameObject _bullet;
    [SerializeField] private int ActiveSubClass = 0;
    //[SerializeField] private GameObject[] _bullet;

    public delegate void SetbulletDelegate(Vector3 position);
    public SetbulletDelegate Setbullet;

    private bool isAttacking = false;
    private void OnEnable()
    {
        FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
    }
    private void OnDisable()
    {
        FindObjectOfType<DebugUI>().ChangeSubClass -= ChangeSubClass;
    }
    void Update()
    {
        UpdatePosition();
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            SpawnBullet();
        }
    }
    private void SpawnBullet() //normal atk
    {

        GameObject bull = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(_firepoint.up * _Bulletforce, ForceMode2D.Impulse);
        Setbullet(_mousepos);
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
                ActiveSubClass = 0;
                break;

            case 1: //Explotion
                ActiveSubClass = 1;
                break;

            case 2: //Posion
                ActiveSubClass = 2;
                break;

            case 3: //Burn
                ActiveSubClass = 3;
                break;

            default:
                break;
        }
    }
}
