using System.Collections;
using UnityEngine;

public class ClassPhysic : CharacterCon
{
    [SerializeField] GameObject[] _bullet;
    [SerializeField] Sprite[] _bulletSprite;

    private GameObject _selectBullet;
    private Sprite _selectSprite;
    private bool isAttacking = false ;

    private void OnEnable()
    {
        InitializeStats();
    }
    protected override void Update()
    {
        base.Update();
        UpdatePosition();
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            SpawnBullet();
            Debug.Log("Att");
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

        _selectSprite = _bulletSprite[ActiveSubClass];

        if (ActiveSubClass == 1)
        {
            GameObject bullleft = Instantiate(_selectBullet, fireRange.position, fireRange.rotation, fireRange.transform);
            bullleft.GetComponent<SpriteRenderer>().sprite = _selectSprite;
            Rigidbody2D rbleft = bullleft.GetComponent<Rigidbody2D>();
            rbleft.AddForce((Quaternion.Euler(0, 0, 15.5f) * fireRange.up) * _Bulletforce, ForceMode2D.Impulse);

            GameObject bullright = Instantiate(_selectBullet, fireRange.position, fireRange.rotation, fireRange.transform);
            bullright.GetComponent<SpriteRenderer>().sprite = _selectSprite;
            Rigidbody2D rbright = bullright.GetComponent<Rigidbody2D>();
            rbright.AddForce((Quaternion.Euler(0, 0, -15.5f) * fireRange.up) * _Bulletforce, ForceMode2D.Impulse);
        }

        GameObject bull = Instantiate(_selectBullet, fireRange.position, fireRange.rotation, fireRange.transform);
        bull.GetComponent<SpriteRenderer>().sprite = _selectSprite;
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(fireRange.up * _Bulletforce, ForceMode2D.Impulse);

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
        if (ID == 2)
        {
            Atk_Speed += 2;
        }
        //FindObjectOfType<DebugUI>().ChangeSubClass -= ChangeSubClass; //when debug finish remove plz comment  
    }

    public void SubscribeChangeSubClass()
    {
        FindObjectOfType<SelectSubclass>().ChangeSubClass += ChangeSubClass;
        Debug.Log("Subscribe in Physics!");
    }
}