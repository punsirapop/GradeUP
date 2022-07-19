using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossLngSkill1 : ActionNode
{
    public GameObject muscleStrongPrefab;
    public int attackTimes = 4;
    public int ringCount = 6;
    public int scaleRing = 3;
    public int spaceRing = 4;
    public float skillDurationForStomp = 1;
    public float skillDelay = .5f;

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
        // Debug.Log(Time.time - startDelayTime > skillDurationForStomp - skillDelay);
        
        if (Time.time - startDelayTime > skillDurationForStomp && muscleStrong != null)
        {
            if (Time.time - startDelayTime > skillDurationForStomp + skillDelay && muscleStrong != null)
            {
                isOdd = !isOdd;
                AttackStomp(isOdd);
                startDelayTime = Time.time;
            }
            else
            {
                AttackStomp(!isOdd, true);
            }
        }

        if (Time.time - startTime > (skillDurationForStomp + skillDelay) * attackTimes + skillDurationForStomp)
        {
            Destroy(muscleStrong);
            return State.Success;
        }
        return State.Running;
    }

    private void MuscleStrong()
    {
        muscleStrong = Instantiate(muscleStrongPrefab, context.transform.position, Quaternion.identity);
        muscleStrong.GetComponent<MuscleStrong>().Setup(scaleRing, ringCount, spaceRing);

        ResetRings();
        startDelayTime = Time.time;
    }

    private void AttackStomp(bool isOdd)
    {
        AttackStomp(isOdd, false);
    }

    private void AttackStomp(bool isOdd, bool warning)
    {
        int indexCount = 1;
        foreach (Component child in muscleStrong.transform)
        {
            if (indexCount++%2 == Convert.ToInt32(isOdd))
            {
                if (warning)
                {
                    child.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(.5f, 0, 0);
                }
                else
                {
                    child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                }
            }
            else
            {
                child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            }
        }
    }

    private void ResetRings()
    {
        foreach (Component child in muscleStrong.transform)
        {
            child.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.clear;
        }
    }
}
