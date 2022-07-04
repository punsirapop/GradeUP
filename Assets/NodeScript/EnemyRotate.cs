using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class EnemyRotate : ActionNode
{
    public float rotateDegree;
    public float duration;
    float startTime;
    private Quaternion target;
    protected override void OnStart() {
        startTime = Time.time;
        target = Quaternion.Euler(0, 0, rotateDegree);
        Debug.Log(target);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, target, Time.deltaTime * duration);

        if (Time.time - startTime > duration)
        {
            Debug.Log("success");
            return State.Success;
        }
        return State.Running;
    }
}
