using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossLngSkill1 : ActionNode
{
    public GameObject muscleStrongPrefab;
    public int attackTimes = 6;
    public int ringCount = 6;
    public int scaleRing = 2;
    public float skillDurationForStomp;

    GameObject muscleStrong;
    bool isOdd;
    float startTime;
    float startDelayTime;

    protected override void OnStart() {
        MuscleStrong();

        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // if (Time.time - startTime > skillDurationForTimes * (attackTimes + 1) / 3)
        if (Time.time - startDelayTime > skillDurationForStomp && muscleStrong != null)
        {
            isOdd = !isOdd;
            AttackStomp();
        }

        if (Time.time - startTime > skillDurationForStomp * attackTimes)
        {
            Destroy(muscleStrong);
            return State.Success;
        }
        return State.Running;
    }

    private void MuscleStrong()
    {
        muscleStrong = Instantiate(muscleStrongPrefab, context.transform.position, Quaternion.identity);
        muscleStrong.GetComponent<MuscleStrong>().Setup(scaleRing, attackTimes, skillDurationForStomp);

        AttackStomp();
    }

    private void AttackStomp()
    {
        int indexCount = 1;
        foreach (Component child in muscleStrong.transform)
        {
            if (indexCount++%2 == Convert.ToInt32(isOdd))
            {
                child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            }
            else
            {
                child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            }
        }
        startDelayTime = Time.time;
    }

    private void ResetRings()
    {
        foreach (Component child in muscleStrong.transform)
        {
            child.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
    }
}
