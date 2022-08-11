using System.Collections;
using UnityEngine;

public class ClassChem : CharacterCon
{
    [SerializeField] private GameObject[] _bullet;
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
        GameObject bull = Instantiate(_selectBullet, fireRange.position, fireRange.rotation, fireRange.transform);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(fireRange.up * _Bulletforce, ForceMode2D.Impulse);
        Setbullet?.Invoke(_mousepos);
        StartCoroutine(OnCooldown());
    }
    IEnumerator OnCooldown()
    {
        float Cooldown = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(Cooldown);
        isAttacking = false;
    }

    protected override void ChangeSubClass(int ID) //for Change Sub-Class 
    {
        base.ChangeSubClass(ID);

        switch (ID)
        {
            case 0: //default
                _selectBullet = _bullet[0];
                break;

            case 1: //Explosion
                _selectBullet = _bullet[1];
                break;

            case 2: //Posion
                _selectBullet = _bullet[2];
                break;

            case 3: //Burn
                _selectBullet = _bullet[3];
                break;

            default:
                break;
        }
        //FindObjectOfType<DebugUI>().ChangeSubClass -= ChangeSubClass; //when debug finish remove plz comment  
    }

    public void SubscribeChangeSubClass()
    {
        FindObjectOfType<SelectSubclass>().ChangeSubClass += ChangeSubClass;
        // Debug.Log("Subscribe in Physics!");
    }
}
