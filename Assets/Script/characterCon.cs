using UnityEngine;

public class characterCon : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Rigidbody2D firepoint;
    [SerializeField] protected Camera cam;

    [SerializeField] protected Transform _firepoint;
    [SerializeField] protected GameObject _bullet;

    private Vector2 _movement;
    private Vector2 _mousepos;

    protected float _Bulletforce = 20f;
    [SerializeField] protected float _movespeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (_movement * _movespeed * Time.fixedDeltaTime));
        firepoint.MovePosition(rb.position + (_movement * _movespeed * Time.fixedDeltaTime));
        Vector2 lookdir = _mousepos - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        firepoint.rotation = angle;
    }

    protected void UpdatePosition()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
