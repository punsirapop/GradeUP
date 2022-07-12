using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SummonWeapon : ActionNode
{
    public float duration = 2;
    public GameObject laserPrefab;
    //*****************************************
    public enemySO attackOwnerStat;
    //*****************************************
    float startTime;

    Vector2 playerPos;
    Vector2 enemyPos;
    Vector2 direction;

    Quaternion playerAngle;

    protected override void OnStart()
    {
        startTime = Time.time;
        playerPos = blackboard.targetPosition;
        enemyPos = context.transform.position;
        direction = (playerPos - enemyPos);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        playerAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject laser = Instantiate(laserPrefab, context.transform.position, playerAngle);
        //************************************************************************************
        laser.GetComponent<AttackHandler>().attackOwner = attackOwnerStat;
        //************************************************************************************
        Destroy(laser, duration);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
            return State.Success;
        }
        return State.Running;
    }

}