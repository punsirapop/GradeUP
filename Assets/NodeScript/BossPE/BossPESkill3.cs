using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class BossPESkill3 : ActionNode
{
    [Header("Movement")]
    public float speed;
    public float stoppingDistance;
    public float acceleration = 40.0f;  

    private Vector2 target;

    protected override void OnStart() {
        //direction = (blackboard.moveToPosition - new Vector2(context.transform.position.x, context.transform.position.y)).normalized;
        //target = MainGame.instance.playerController.transform;
        target = blackboard.moveToPosition;


        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.updateRotation = false;
        context.agent.updateUpAxis = false;
        context.agent.destination = target;
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
        context.agent.destination = target;
        return State.Running;
    }
}
