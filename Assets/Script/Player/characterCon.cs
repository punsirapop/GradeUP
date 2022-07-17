using System;
using UnityEngine;

public class characterCon : StatusManager
{
    public MoveToNextRoom moveable;

    public static bool isIFramed = false;

    public static event Action<GameObject> OnCollectItem;
    public static event Action<GameObject> OnHit;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Rigidbody2D firepoint;
    [SerializeField] protected Camera cam;
    [SerializeField] protected Transform _firepoint;

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
        rb.MovePosition(rb.position + (_movement * MoveSpeed * Time.deltaTime));
        firepoint.MovePosition(rb.position + (_movement * MoveSpeed * Time.deltaTime));
        Vector2 lookdir = _mousepos - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        firepoint.rotation = angle;
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Item":
                OnCollectItem?.Invoke(collision.gameObject);
                break;
            case "EnemyAttack":
                if (!isIFramed)
                {
                    OnHit?.Invoke(collision.gameObject);
                }
                break;
            case "Enemy":
                if (!isIFramed)
                {
                    OnHit?.Invoke(collision.gameObject);
                }
                break;
        }
    }
}
