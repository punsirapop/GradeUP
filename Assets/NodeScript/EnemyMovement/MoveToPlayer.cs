using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToPlayer : ActionNode
{
    [Header("Movement")]
    public float speed;
    public float stoppingDistance;
    public float acceleration = 40.0f;

    private Transform target;

    protected override void OnStart() {
        
        target = MainGame.instance.playerController.transform;
        
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.updateRotation = false;
        context.agent.updateUpAxis = false;
        context.agent.destination = target.position;
        context.agent.acceleration = acceleration;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.agent.pathPending)
        {
            Debug.Log("Running");
            return State.Running;
        }

        if (context.agent.remainingDistance < stoppingDistance)
        {
            context.agent.destination = context.transform.position;
            return State.Success;
        }
        context.agent.destination = target.position;
        return State.Running;
    }
}
