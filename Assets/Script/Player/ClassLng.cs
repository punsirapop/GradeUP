using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ClassLng : CharacterCon
{
    [SerializeField] GameObject hitWave , hitSwing;
    [SerializeField] List<Sprite> waveSprite = new List<Sprite>();
    [SerializeField] List<Sprite> swingSprite = new List<Sprite>();
    bool isAttacking = false, isShooting = false;
    readonly object attackLock = new object();

    public static event Action ScreenHit;

    ObjectPool<GameObject> hitWavePool;
    List<GameObject> hitSwingList = new List<GameObject>();

    float swingAngle = 0f;
    private void OnEnable()
    {
        InitializeStats();
        // FindObjectOfType<DebugUI>().ChangeSubClass += ChangeSubClass;
        hitWavePool = new ObjectPool<GameObject>(() => { return Instantiate(hitWave, fireRange); },
            wave => { wave.gameObject.SetActive(true);
                wave.transform.position = fireRange.position;
                wave.transform.rotation = fireRange.rotation;
                wave.transform.localScale = fireRange.localScale;
                wave.GetComponent<SpriteRenderer>().sprite = waveSprite[UnityEngine.Random.Range(0, waveSprite.Count)];
            },
            wave => { wave.gameObject.SetActive(false); },
            wave => { Destroy(wave.gameObject); },
            true, 10, 20);

        for (int i = 0; i < 6; i++)
        {
            Vector3 position = Quaternion.AngleAxis(i * 360 / 6, Vector3.forward) * fireRange.position;
            Quaternion rotation = Quaternion.AngleAxis(i * 360 / 6, Vector3.forward);
            GameObject hitBox = Instantiate(hitSwing, position, rotation);
            hitBox.GetComponent<SpriteRenderer>().sprite = swingSprite[i];
            hitBox.SetActive(false);
            hitSwingList.Add(hitBox);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        lock (attackLock)
        {
            isAttacking = true;
            //fireRange.position = transform.position + transform.rotation * new Vector3(0f, .5f);
            switch (ActiveSubClass)
            {
                case 0:
                    NormalAttack();
                    break;
                case 1:
                    NormalAttack();
                    break;
                case 2:
                    StartCoroutine(OrbitAttack());
                    break;
                case 3:
                    ScreenAttack();
                    break;
            }
            Debug.Log("Attacked");
        }
    }

    private void NormalAttack() //normal atk
    {
        isShooting = true;
        /*
        GameObject bull = Instantiate(hitWave, fireRange.position, fireRange.rotation, fireRange.transform);
        bull.GetComponent<SpriteRenderer>().sprite = waveSprite[UnityEngine.Random.Range(0, waveSprite.Count)];
        */
        GameObject wave = hitWavePool.Get();
        Rigidbody2D rb = wave.GetComponent<Rigidbody2D>();
        rb.AddForce(fireRange.up * 5f, ForceMode2D.Impulse);
        StartCoroutine(DelayDestroy(wave));
        StartCoroutine(OnCooldown());
    }

    IEnumerator OnCooldown()
    {
        float Cooldown = 5 / (2 * Atk_Speed);
        yield return new WaitForSeconds(Cooldown);
        isShooting = false;
        isAttacking = false;
        yield break;
    }

    IEnumerator DelayDestroy(GameObject wave)
    {
        float delay = .5f;
        yield return new WaitForSeconds(delay);
        hitWavePool.Release(wave);
    }

    IEnumerator OrbitAttack()
    {
        isShooting = true;

        foreach (GameObject hitSwing in hitSwingList)
        {
            hitSwing.SetActive(true);
        }

        // Swing
        float swingTime = 5 / Atk_Speed;
        for (float time = 0; time < swingTime; time += Time.deltaTime)
        {
            foreach (GameObject hitBox in hitSwingList)
            {
                hitBox.transform.position = transform.position + hitBox.transform.rotation *
                    Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector2(3f, 0f);
                swingAngle += 30 * Time.deltaTime / swingTime;
            }
            yield return null;
        }

        foreach (GameObject hitSwing in hitSwingList)
        {
            hitSwing.SetActive(false);
        }

        isShooting = false;
        isAttacking = false;
        yield break;
    }
    void ScreenAttack()
    {
        ScreenHit?.Invoke();
        StartCoroutine(OnCooldown());
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

    public void SubscribeChangeSubClass()
    {
        FindObjectOfType<SelectSubclass>().ChangeSubClass += ChangeSubClass;
        // Debug.Log("Subscribe in LNG!");
    }
}
