using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassArt : CharacterCon
{
    [SerializeField] List<GameObject> _hitSwing;
    [SerializeField] GameObject _hitDash;
    [SerializeField] List<GameObject> _bullet;
    [SerializeField] List<Material> _trail;
    [SerializeField] LineRenderer lineRenderer;

    bool isAttacking = false, isShooting = false, isDashing = false;
    List<GameObject> hitSwing = new List<GameObject>();
    GameObject hitDash;

    private static int playerColor;
    public static int PlayerColor => playerColor;

    readonly object attackLock = new object();

    private void OnEnable()
    {
        InitializeStats();
        // FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;

        foreach (GameObject hitbox in _hitSwing)
        {
            GameObject genHitBox = Instantiate(hitbox, fireRange.position,
                Quaternion.AngleAxis(90f, Vector3.forward) * fireRange.rotation, firepoint.transform);
            genHitBox.SetActive(false);
            hitSwing.Add(genHitBox);
        }

        hitDash = Instantiate(_hitDash, fireRange.position, fireRange.rotation, fireRange.transform);
        hitDash.GetComponent<SpriteRenderer>().enabled = false;
        hitDash.SetActive(false);

        /*
        foreach (GameObject hitbox in _hitDash)
        {
            GameObject genHitBox = Instantiate(hitbox, fireRange.position, fireRange.rotation, firepoint.transform);
            genHitBox.SetActive(false);
            hitDash.Add(genHitBox);
        }
        */
    }
    protected override void Update()
    {
        base.Update();
        if (!isAttacking && Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void RandomColor()
    {
        playerColor = UnityEngine.Random.Range(0, 3);
    }

    private void Attack()
    {
        lock (attackLock)
        {
            isAttacking = true;
            switch (activeSubClass)
            {
                case 0:
                    StartCoroutine(NormalAttack());
                    break;
                case 1:
                    StartCoroutine(NormalAttack());
                    break;
                case 2:
                    StartCoroutine(PaintballGun());
                    break;
                case 3:
                    StartCoroutine(DashAttack());
                    break;
            }
            Debug.Log("Attacked");
        }
    }

    IEnumerator NormalAttack()
    {
        // Randomize color of attack
        RandomColor();
        hitSwing[playerColor].SetActive(true);
        /*
        rb.velocity = Vector2.zero;
        // Swing
        float swingAngle = 0f;
        float swingTime = 5 / (2 * Atk_Speed);
        for (float time = 0; time < swingTime; time += Time.deltaTime)
        {
            hitBox.transform.position = transform.position + _firepoint.rotation *
                Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector2(2f, 0f);
            swingAngle += 180 * Time.deltaTime / swingTime;
            yield return null;
        }
        */
        yield return new WaitForSeconds(5 / (2 * Atk_Speed));
        hitSwing[playerColor].SetActive(false);
        StartCoroutine(OnCooldown());
        yield break;
    }

    IEnumerator PaintballGun()
    {
        isShooting = true;

        //GameObject hitBox = Instantiate(_bullet, _firepoint.position, _firepoint.rotation, bullets.transform);
        // Randomize color of attack
        RandomColor();
        GameObject bull = Instantiate(_bullet[playerColor], fireRange.position, fireRange.rotation);
        Rigidbody2D rb = bull.GetComponent<Rigidbody2D>();
        rb.AddForce(fireRange.up * _Bulletforce, ForceMode2D.Impulse);

        StartCoroutine(OnCooldown());
        isShooting = false;
        yield break;
    }
    IEnumerator DashAttack()
    {
        GameObject hitMaxRange = new GameObject("hitMaxRange");
        hitMaxRange.transform.position = fireMaxRange.transform.position;
        lineRenderer.enabled = true;
        Vector3 startPos = transform.position;
        lineRenderer.SetPosition(0, startPos);

        RandomColor();
        hitDash.SetActive(true);
        lineRenderer.material = _trail[playerColor];

        /*
        GameObject hitMaxRange = Instantiate(hitSwing, fireRange.position, fireRange.rotation);
        hitMaxRange.tag = "Untagged";
        hitMaxRange.transform.position += _firepoint.rotation * new Vector2(0f, 3f);
        Destroy(hitMaxRange.GetComponent<SpriteRenderer>());
        */

        float spd = 5f;

        isIFramed = true;
        isDashing = true;
        StartCoroutine(WaitForDashEnd());
        while (Vector2.Distance(transform.position, hitMaxRange.transform.position) > .05f && isDashing)
        {
            transform.position = Vector2.Lerp(transform.position, hitMaxRange.transform.position, Time.deltaTime * spd);
            lineRenderer.SetPosition(1, fireRange.transform.position);
            yield return new WaitForFixedUpdate();
        }
        isIFramed = false;
        lineRenderer.enabled = false;
        hitDash.SetActive(false);
        Destroy(hitMaxRange);

        rb.velocity = Vector2.zero;
        StartCoroutine(OnCooldown());
        yield break;
    }

    IEnumerator WaitForDashEnd()
    {
        yield return new WaitForSeconds(1f);
        isDashing = false;
    }

    IEnumerator OnCooldown()
    {
        float Cooldown = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(Cooldown);
        isAttacking = false;
    }

    protected override void FixedUpdate()
    {
        if (!isAttacking || isShooting)
        {
            GetPosition();
            UpdatePosition();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}