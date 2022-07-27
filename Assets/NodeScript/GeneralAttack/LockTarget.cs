using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class LockTarget : ActionNode
{
    public bool isTargetPlayer;
    public float duration = 0.5f;
    float startTime;
    Vector2 playerPos;
    protected override void OnStart() {
        startTime = Time.time;

        if (isTargetPlayer)
        {
            playerPos = MainGame.instance.playerController.transform.position;
            blackboard.targetPosition = playerPos;
        }

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
