using System.Collections;
using UnityEngine;

public class classPE : characterCon
{
    [SerializeField]
    float aspd;
    readonly object clickLock = new object();
    public int subclass;
    bool isAttacking = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && Input.GetButtonDown("Fire1"))
        {
            Punch();
        }
    }

    private void Punch()
    {
        lock (clickLock)
        {
            isAttacking = true;
            switch (subclass)
            {
                case 0:
                    StartCoroutine(Punch0());
                    break;
                case 1:
                    //StartCoroutine(Punch1());
                    break;
                case 2:
                    //StartCoroutine(Punch2());
                    break;
                case 3:
                    //StartCoroutine(Punch3());
                    break;
            }
            Debug.Log("Punched");

        }
    }
    IEnumerator Punch0()
    {
        if (isAttacking)
        {
            // Generate hitbox
            GameObject hitBox = Instantiate(_bullet, _firepoint.transform);
            // Forced stop moving
            rb.velocity = Vector2.zero;
            // Set reload time
            float punchTime = 5 / (2 * aspd);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            isAttacking = false;
        }
    }
    protected override void FixedUpdate()
    {
        if (!isAttacking)
        {
            GetPosition();
            UpdatePosition();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }
    private void ChangeSubClass(int ID) //for Change Sub-Class 
    {
        switch (ID)
        {
            case 0: //default
                break;

            case 1: //
                break;

            case 2: //
                break;

            case 3: //
                break;

            default:
                break;
        }
    }
}
