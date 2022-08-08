using System;
using UnityEngine;

public class CharacterCon : StatusManager
{
    public MoveToNextRoom moveable;

    public static bool isIFramed = false;

    public static event Action<GameObject> OnCollectItem;
    public static event Action<GameObject> OnHit;
    public static event Action<GameObject> OnPoisoned;
    public static event Action<GameObject> OnNearNPC;
    public static event Action<GameObject> OnLeaveNPC;
    public static event Action OnInteractNPC;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Rigidbody2D firepoint;
    [SerializeField] protected Camera cam;
    [SerializeField] protected Transform fireRange;
    [SerializeField] protected Transform fireMaxRange;

    protected Vector2 _movement;
    protected Vector2 _mousepos;

    protected float _Bulletforce = 20f;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        cam = Camera.FindObjectOfType<Camera>();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");
            OnInteractNPC?.Invoke();
        }
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
        Debug.Log("collide with - " + collision.tag);
        if (collision.CompareTag("Item"))
        {
            Debug.Log("Got Item");
            OnCollectItem?.Invoke(collision.gameObject);
        }
        else if (collision.CompareTag("NPC"))
        {
            OnNearNPC?.Invoke(collision.gameObject);
        }
        else if (!isIFramed)
        {
            switch (collision.tag)
            {
                case "EnemyAttack":
                    Debug.Log("Got Hit");
                    OnHit?.Invoke(collision.gameObject);
                    break;
                case "Enemy":
                    Debug.Log("Got Hit");
                    OnHit?.Invoke(collision.gameObject);
                    break;
                case "EnemyPoison":
                    Debug.Log("Got Poisoned");
                    OnPoisoned?.Invoke(collision.gameObject);
                    break;
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            OnLeaveNPC?.Invoke(collision.gameObject);
        }
    }
}