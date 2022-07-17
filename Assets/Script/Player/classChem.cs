using System.Collections;
using UnityEngine;

public class classChem : characterCon
{
    [SerializeField] private GameObject[] _bullet;
    [SerializeField] private int ActiveSubClass = 0;
    private GameObject _selectBullet;

    public delegate void SetbulletDelegate(Vector3 position);
    public SetbulletDelegate Setbullet;

    private bool isAttacking = false;
    private void OnEnable()
    {
        InitializeStats();
        // FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
        // ****For Debug Purpose****
        _selectBullet = _bullet[ActiveSubClass];
        // *************************
        // _selectBullet = _bullet[0];
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
        isAttacking = true;
        GameObject bull = Instantiate(_selectBullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(_firepoint.up * _Bulletforce, ForceMode2D.Impulse);
        Setbullet?.Invoke(_mousepos);
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
                ActiveSubClass = 0;
                break;

            case 1: //Explotion
                ActiveSubClass = 1;
                _selectBullet = _bullet[1];
                break;

            case 2: //Posion
                ActiveSubClass = 2;
                _selectBullet = _bullet[2];
                break;

            case 3: //Burn
                ActiveSubClass = 3;
                _selectBullet = _bullet[3];
                break;

            default:
                break;
        }
        //FindObjectOfType<DebugUI>().ChangeSubClass -= ChangeSubClass; //when debug finish remove plz comment  
    }
}
