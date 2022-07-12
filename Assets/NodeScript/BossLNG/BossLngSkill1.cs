using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossLngSkill1 : ActionNode
{
    public GameObject muscleStrongPrefab;
    public int attackTimes = 6;
    public float skillDurationForTimes;

    GameObject muscleStrong;
    float startTime;

    protected override void OnStart() {
        MuscleStrong();

        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > skillDurationForTimes * (attackTimes + 1) / 3)
        {
            return State.Success;
        }
        return State.Running;
    }

    private void MuscleStrong()
    {
        muscleStrong = Instantiate(muscleStrongPrefab, context.transform.position, Quaternion.identity);
        muscleStrong.GetComponent<MuscleStrong>().Setup(attackTimes, skillDurationForTimes);
    }
}
