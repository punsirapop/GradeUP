using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class LaserAttack : ActionNode
{
    public float duration = 2;
    public GameObject laserPrefab;
    float startTime;

    protected override void OnStart() {
        startTime = Time.time;
        GameObject laser = Instantiate(laserPrefab, context.transform.position, Quaternion.identity);
        Destroy(laser, duration);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (Time.time - startTime > duration)
        {
            return State.Success;
        }
        return State.Running; 
    }
}
