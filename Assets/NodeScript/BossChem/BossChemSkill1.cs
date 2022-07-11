using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossChemSkill1 : ActionNode
{
    [SerializeField] GameObject smokePowerPrefab;

    public float smokePowerDuration = 2f;
    public float smokePowerForce = 10f;
    public float smokePowerLength = 1f;
    public float smokePowerSize = 2f;

    private Vector2 playerPos;
    private Transform enemy;
    private Vector2 enemyPos;

    private float distanceX;
    private float distanceY;
    
    private float startTime;

    protected override void OnStart() {
        playerPos = MainGame.instance.playerController.transform.position;
        enemy = context.transform;

        SmokePowerAttack();
        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > smokePowerDuration)
        {
            return State.Success;
        }
        return State.Running;
    }

    private void SmokePowerAttack()
    {
        enemyPos = new Vector2(enemy.position.x, enemy.position.y);
        
        distanceX = enemyPos.x - playerPos.x;
        distanceY = enemyPos.y - playerPos.y;
        
        float angle = Mathf.Atan2(distanceY, distanceX) * Mathf.Rad2Deg - 90f * -1;
        Quaternion enemyAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        
        SpawnSmokePower(enemyPos, enemyAngle);
    }

    private void SpawnSmokePower(Vector2 enemyPos, Quaternion enemyAngle)
    {
        GameObject dealthSmoke = Instantiate(smokePowerPrefab, enemyPos, enemyAngle);
        dealthSmoke.transform.localScale = new Vector2(smokePowerSize, smokePowerLength);
        FinishSmokePower(dealthSmoke);
    }

    private void FinishSmokePower(GameObject dealthSmoke)
    {
        Destroy(dealthSmoke, smokePowerDuration);
    }
}
