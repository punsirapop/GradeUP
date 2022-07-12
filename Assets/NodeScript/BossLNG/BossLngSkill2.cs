using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossLngSkill2 : ActionNode
{
    public GameObject bulletPrefab;

    public float charShootDuration = 1.5f;
    public float charShootForce = 5f;

    public int maxCharShootTimes = 3;
    public int maxDirection = 8;

    public float turnSpeed = 10f;

    private Vector2 enemyPos;
    float startTime;
    GameObject parent;
    int shootTimes = 0;

    protected override void OnStart() {
        enemyPos = context.transform.position;
        shootTimes = 0;
        // CharShootManyDirection();
        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > charShootDuration * maxCharShootTimes)
        {
            return State.Success;
        }
        
        if (shootTimes < maxCharShootTimes && Time.time - startTime > shootTimes)
        {
            CharShootManyDirection();
        }

        return State.Running;
    }

    private void CharShootManyDirection()
    {
        shootTimes++;
        parent = new GameObject("SpawnCharShoots");
        SpawnCharShoots spawn = parent.AddComponent<SpawnCharShoots>();
        spawn.Setup(enemyPos, turnSpeed);

        for (int i = 0; i < maxDirection; i++)
        {
            Quaternion enemyAngle = Quaternion.AngleAxis(i * (360f / maxDirection), Vector3.forward);
            Vector2 attackPos = new Vector2(enemyPos.x, enemyPos.y);
            SpawnCharShoot(attackPos, enemyAngle);
        }
        FinishAttack();
    }

    private void SpawnCharShoot(Vector2 attackPos, Quaternion enemyAngle)
    {
        GameObject bullet = Instantiate(bulletPrefab, attackPos, enemyAngle, parent.transform);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(rb.transform.up * charShootForce, ForceMode2D.Impulse);
    }

    private void FinishAttack()
    {
        Destroy(parent, charShootDuration);
    }
}
