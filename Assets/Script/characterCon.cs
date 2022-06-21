using UnityEngine;

public class characterCon : StatusManager
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Rigidbody2D firepoint;
    [SerializeField] protected Camera cam;

    [SerializeField] protected Transform _firepoint;
    //[SerializeField] protected GameObject _bullet;

    protected Vector2 _movement;
    protected Vector2 _mousepos;

    protected float _Bulletforce = 20f;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        cam = Camera.FindObjectOfType<Camera>();
    }
    protected virtual void FixedUpdate()
    {
        GetPosition();
        UpdatePosition();
    }

    protected void GetPosition()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    protected void UpdatePosition()
    {
        rb.MovePosition(rb.position + (_movement * _movespeed * Time.fixedDeltaTime));
        firepoint.MovePosition(rb.position + (_movement * _movespeed * Time.fixedDeltaTime));
        Vector2 lookdir = _mousepos - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        firepoint.rotation = angle;
    }
}
