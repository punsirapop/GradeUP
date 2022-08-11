using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassPE : CharacterCon
{
    [SerializeField] List<GameObject> _hitBox;
    bool isAttacking = false ,isCharging = false, isDashing = false;
    readonly object clickLock = new object();

    List<GameObject> hitBox = new List<GameObject>();

    private void OnEnable()
    {
        InitializeStats();
        // FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
        foreach(GameObject obj in _hitBox)
        {
            GameObject newHitBox = Instantiate(obj, fireRange.position, fireRange.rotation, fireRange.transform);
            newHitBox.SetActive(false);
            hitBox.Add(newHitBox);
        }

        hitBox[0].transform.rotation *= Quaternion.AngleAxis(90f, Vector3.forward);
    }
    protected override void Update()
    {
        base.Update();
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
            if(activeSubClass == 1)
            {
                StartCoroutine(ChargePunch());
            }
            else
            {
                StartCoroutine(NormalPunch());
            }
            
            Debug.Log("Punched");

        }
    }
    IEnumerator NormalPunch()
    {
        if (isAttacking)
        {
            int subSpeed = 1;
            int hitboxUse = 0;
            if(activeSubClass == 2)
            {
                subSpeed = 3;
            }
            if(activeSubClass == 3)
            {
                hitboxUse = 2;
                animator.SetTrigger("isBaseballAttack");
            }
            else
            {
                animator.SetBool("isAttack", true);
                animator.SetTrigger("isAttackT");
            }
            // Generate hitbox
            hitBox[hitboxUse].SetActive(true);
            // Forced stop moving
            rb.velocity = Vector2.zero;
            // Set reload time
            float punchTime = 5 / (2 * Atk_Speed * subSpeed);
            yield return new WaitForSeconds(punchTime);
            hitBox[hitboxUse].SetActive(false);
            StartCoroutine(OnCooldown());
        }
    }
    IEnumerator ChargePunch()
    {
        if (isAttacking)
        {
            // Initiate charging sequence
            isCharging = true;
            animator.SetBool("isGuard", true);
            // Generate charge destination indicator
            GameObject hitMaxRange = Instantiate(_hitBox[0], fireRange.position,
                Quaternion.AngleAxis(90f, Vector3.forward) * fireRange.rotation, firepoint.transform);
            hitMaxRange.GetComponent<Collider2D>().enabled = false;
            while (Input.GetMouseButton(0))
            {
                // Keep extending indicator until set position
                if (Vector2.Distance(transform.position, hitMaxRange.transform.position) < 5f)
                {
                    hitMaxRange.transform.position += fireRange.rotation * new Vector2(0f, .05f);
                }
                yield return new WaitForFixedUpdate();
            }
            hitMaxRange.transform.SetParent(null);
            hitMaxRange.GetComponent<SpriteRenderer>().enabled = false;

            // End charging sequence
            isCharging = false;
            animator.SetBool("isAttack", true);
            animator.SetBool("isGuard", false);
            isIFramed = true;
            isDashing = true;
            // Set dash speed
            float spd = 5f;
            // Generate hitbox
            hitBox[1].SetActive(true);
            // Dash player until reaching destination
            StartCoroutine(WaitForDashEnd());
            while (Vector2.Distance(transform.position, hitMaxRange.transform.position) > .05f && isDashing)
            {
                transform.position = Vector2.Lerp(transform.position, hitMaxRange.transform.position, Time.deltaTime * spd);
                yield return new WaitForFixedUpdate();
            }
            isIFramed = false;
            hitBox[1].SetActive(false);
            Destroy(hitMaxRange);
            // Set reload time
            StartCoroutine(OnCooldown());
        }
    }

    IEnumerator WaitForDashEnd()
    {
        yield return new WaitForSeconds(1f);
        isDashing = false;
    }
    /*
    IEnumerator SwingAtk()
    {
        if (isAttacking)
        {
            GameObject hitBox = Instantiate(hitPunch, fireRange.position, fireRange.rotation, firepoint.transform);
            hitBox.tag = "Knock";
            rb.velocity = Vector2.zero;
            // Set swing angle, start range, and time
            float swingAngle = -90f, swingRange = 180, swingTime = 5 / (2 * Atk_Speed);
            // Move hitbox in circular motion within swing time limit
            for (float time = 0; time < swingTime; time += Time.deltaTime)
            {
                hitBox.transform.SetPositionAndRotation(transform.position + fireRange.rotation *
                    Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector3(0f, 2f), fireRange.rotation * Quaternion.AngleAxis(swingAngle, Vector3.forward));
                swingAngle += swingRange * Time.deltaTime / swingTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            Destroy(hitBox);
            StartCoroutine(OnCooldown());
        }
    }
    */
    IEnumerator OnCooldown()
    {
        float Cooldown = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(Cooldown);
        isAttacking = false;
        animator.SetBool("isAttack", false);
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
            if (isCharging)
            {
                _mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            }
            rb.velocity = Vector2.zero;
        }
    }

    public void SubscribeChangeSubClass()
    {
        FindObjectOfType<SelectSubclass>().ChangeSubClass += ChangeSubClass;
        // Debug.Log("Subscribe in PE!");
    }
}
