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

    Vector2 destination;

    protected override void OnStart() {
        // blackboard.moveToPosition = new Vector2(positionX, positionY);
        // direction = (blackboard.moveToPosition - new Vector2(context.transform.position.x, context.transform.position.y)).normalized;

        destination = new Vector2(positionX, positionY);
        direction = (destination - new Vector2(context.transform.position.x, context.transform.position.y)).normalized;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (Vector2.Distance(context.transform.position, destination) < 0.5f)
        // if (Vector2.Distance(context.transform.position, destination) < speed * 0.02f)
        {
            return State.Success;
        }
        // context.physics.MovePosition(context.physics.position + speed * Time.fixedDeltaTime * direction);

        float distanceX = destination.x - context.transform.position.x;
        float distanceY = destination.y - context.transform.position.y;
        
        Vector2 direction = new Vector2(Mathf.Sign(distanceX) * 1f, Mathf.Sign(distanceY) * 1f);
        // context.physics.MovePosition(context.physics.position + speed * Time.fixedDeltaTime * direction);
        context.transform.position = ((Vector2) context.transform.position) + speed * Time.deltaTime * direction;
        return State.Running;
    }
}
