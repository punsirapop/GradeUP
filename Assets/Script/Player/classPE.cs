using System.Collections;
using UnityEngine;

public class classPE : characterCon
{
    [SerializeField] GameObject hitPunch, hitSwing;
    bool isAttacking = false ,isCharging = false;
    readonly object clickLock = new object();
    [SerializeField] protected GameObject _bullet;

    private void OnEnable()
    {
        InitializeStats();
        // FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
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
            switch (ActiveSubClass)
            {
                case 0:
                    StartCoroutine(NormalPunch());
                    break;
                case 1:
                    StartCoroutine(ChargePunch());
                    break;
                case 2:
                    StartCoroutine(SpeedPunch());
                    break;
                case 3:
                    StartCoroutine(SwingAtk());
                    break;
            }
            Debug.Log("Punched");

        }
    }
    IEnumerator NormalPunch()
    {
        if (isAttacking)
        {
            // Generate hitbox
            GameObject hitBox = Instantiate(_bullet, fireRange.transform);
            hitBox.tag = "Knock";
            // Forced stop moving
            rb.velocity = Vector2.zero;
            // Set reload time
            float punchTime = 5 / (2 * Atk_Speed);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            isAttacking = false;
        }
    }
    IEnumerator ChargePunch()
    {
        if (isAttacking)
        {
            // Initiate charging sequence
            isCharging = true;
            // Generate charge destination indicator
            GameObject hitMaxRange = Instantiate(hitSwing, fireRange.position, fireRange.rotation, firepoint.transform);
            hitMaxRange.tag = "Untagged";
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
            // End charging sequence
            isCharging = false;
            // Set dash speed
            float spd = 5f;
            // Generate hitbox
            GameObject hitBox = Instantiate(hitPunch, fireRange.position,
                Quaternion.AngleAxis(90f, Vector3.forward) * fireRange.rotation, firepoint.transform);
            hitBox.tag = "Knock";
            // Dash player until reaching destination
            isIFramed = true;
            while (Vector2.Distance(transform.position, hitMaxRange.transform.position) > .05f)
            {
                transform.position = Vector2.Lerp(transform.position, hitMaxRange.transform.position, Time.deltaTime * spd);
                yield return new WaitForFixedUpdate();
            }
            isIFramed = false;
            Destroy(hitMaxRange);
            Destroy(hitBox);
            // Set reload time
            StartCoroutine(OnCooldown());
        }
    }
    IEnumerator SpeedPunch()
    {   //will Change aSpd and will use NormalPunch() instead
        if (isAttacking)
        {
            GameObject hitBox = Instantiate(hitPunch, fireRange.position, fireRange.rotation, firepoint.transform);
            hitBox.tag = "Knock";
            rb.velocity = Vector2.zero;
            float punchTime = 5 / (2 * Atk_Speed * 3);
            yield return new WaitForSeconds(punchTime);
            Destroy(hitBox);
            yield return new WaitForSeconds(punchTime);
            isAttacking = false;
        }
    }
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
    IEnumerator OnCooldown()
    {
        float Cooldown = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(Cooldown);
        isAttacking = false;
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
}
