using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossChemNormal : ActionNode
{
    public GameObject poisonPrefab;
    public GameObject poisonPoolPrefab;

    public float poisonDuration = .5f;
    public float poisonForce = 10f;
    public float poisonPoolDuration = 2f;

    private Vector2 playerPos;
    private Vector2 enemyPos;

    private float distanceX;
    private float distanceY;

    private float startTime;

    private GameObject poison;
    private Vector2 poisonPoolPos;

    protected override void OnStart() {
        playerPos = MainGame.instance.playerController.transform.position;
        enemyPos = context.transform.position;

        distanceX = enemyPos.x - playerPos.x;
        distanceY = enemyPos.y - playerPos.y;

        PoisonAttack();
        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > poisonDuration)
        {
            FinishPoison();
            SpawnPoisonPool();
            return State.Success;
        }
        return State.Running;
    }

    private void PoisonAttack()
    {
        enemyPos = new Vector2(enemyPos.x, enemyPos.y);
        float angle = Mathf.Atan2(distanceY, distanceX) * Mathf.Rad2Deg - 90f;
        Quaternion enemyAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        // context.transform.rotation = enemyAngle;
        SpawnPoison(enemyPos, enemyAngle);
    }

    private void SpawnPoison(Vector2 enemyPos, Quaternion enemyAngle)
    {
        poison = Instantiate(poisonPrefab, enemyPos, enemyAngle);
        Rigidbody2D rb = poison.GetComponent<Rigidbody2D>();
        rb.AddForce(rb.transform.up * -1 * poisonForce, ForceMode2D.Impulse);
    }

    private void SpawnPoisonPool()
    {
        GameObject poisonPool = Instantiate(poisonPoolPrefab, poisonPoolPos, Quaternion.identity);
        FinishPosionPool(poisonPool);
    }

    private void FinishPosionPool(GameObject poisonPool)
    {
        Destroy(poisonPool, poisonPoolDuration);
    }

    private void FinishPoison()
    {
        poisonPoolPos = poison.transform.position;
        Destroy(poison);
    }
}
