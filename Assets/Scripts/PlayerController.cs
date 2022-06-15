using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Rigidbody2D firepoint;
    [SerializeField] protected Camera cam;

    [SerializeField] protected Transform _firepoint;
    [SerializeField] protected GameObject _bullet;

    protected Vector2 _movement;
    protected Vector2 _mousepos;

    protected float _Bulletforce = 20f;
    [SerializeField] protected float _movespeed = 5f;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    protected void UpdatePosition()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        _mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    void FixedUpdate()
    {
        float angle = Mathf.Atan2(_mousepos.y, _mousepos.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.velocity = _movement * Time.deltaTime * 500f;
        _firepoint.position = transform.position + transform.rotation * new Vector3(0f, 1.5f);
    }

    /*
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (_movement * _movespeed * Time.fixedDeltaTime));
        firepoint.MovePosition(rb.position + (_movement * _movespeed * Time.fixedDeltaTime));
        Vector2 lookdir = _mousepos - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        firepoint.rotation = angle;
    }

    protected virtual void UpdatePosition()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    */
}