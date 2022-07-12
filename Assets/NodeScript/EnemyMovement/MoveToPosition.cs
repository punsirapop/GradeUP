using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class MoveToPosition : ActionNode
{
    public float speed = 5;
    public Vector2 direction;
    public float positionX;
    public float positionY;

    protected override void OnStart() {
        blackboard.moveToPosition = new Vector2(positionX, positionY);
        direction = (blackboard.moveToPosition - new Vector2(context.transform.position.x, context.transform.position.y)).normalized;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (Vector2.Distance(context.transform.position, blackboard.moveToPosition) < speed * 0.02f)
        {
            return State.Success;
        }
        context.physics.MovePosition(context.physics.position + speed * Time.fixedDeltaTime * direction);
        return State.Running;
    }
}
