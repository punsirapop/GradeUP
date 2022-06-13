using UnityEngine;

public class characterCon : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D firepoint;
    [SerializeField] private Camera cam;

    [SerializeField] private Transform _firepoint;
    [SerializeField] private GameObject _bullet;

    private Vector2 _movement;
    private Vector2 _mousepos;

    private float _Bulletforce = 20f;
    [SerializeField] private float _movespeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _mousepos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            SpawnBullet();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (_movement * _movespeed * Time.fixedDeltaTime));
        firepoint.MovePosition(rb.position + (_movement * _movespeed * Time.fixedDeltaTime));
        Vector2 lookdir = _mousepos - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        firepoint.rotation = angle;
    }

    private void SpawnBullet()
    {
        GameObject bull = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(_firepoint.up * _Bulletforce, ForceMode2D.Impulse);

        /*for subclass
         * 
        GameObject bull2 = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb2 = bull2.GetComponent<Rigidbody2D>();
        rb2.AddForce((Quaternion.Euler(0, 0, -15.5f) * _firepoint.up) * _Bulletforce, ForceMode2D.Impulse);

        GameObject bull = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce((Quaternion.Euler(0, 0, 15.5f) * _firepoint.up) * _Bulletforce, ForceMode2D.Impulse);*/

    }


}
