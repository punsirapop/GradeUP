    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToPosition : ActionNode
{
    public float speed = 5;
    //private Transform target;
    public float stoppingDistance;
    public float acceleration;

    public enum EnemyMoveToPosition
    {
        selectPosition,
        randomPosition
    }

    public EnemyMoveToPosition enemyMoveToPosition;
    [Header("Select Position")]
    public float positionX;
    public float positionY;

    public float randomMaxPosition;
    private Vector3 destination;

    protected override void OnStart() {
        //target.position = new Vector3(positionX, positionY);
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;

        context.agent.updateRotation = false;
        context.agent.updateUpAxis = false;

        switch (enemyMoveToPosition)
        {
            case EnemyMoveToPosition.selectPosition:
                destination = new Vector3(positionX, positionY);
                break;
            case EnemyMoveToPosition.randomPosition:
                Debug.Log("random!");
                float randX = Random.Range(-randomMaxPosition, randomMaxPosition);
                float randY= Random.Range(-randomMaxPosition, randomMaxPosition);  
                destination = new Vector3(context.transform.position.x + randX, context.transform.position.y + randY);
                Debug.Log(destination);
                break;
        }

        context.agent.destination = destination;
        context.agent.acceleration = acceleration;

        //blackboard.moveToPosition = new Vector2(positionX, positionY);
        //direction = (blackboard.moveToPosition - new Vector2(context.transform.position.x, context.transform.position.y)).normalized;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        //if (Vector2.Distance(context.transform.position, blackboard.moveToPosition) < speed * 0.02f)
        //{
        //    return State.Success;
        //}
        //context.physics.MovePosition(context.physics.position + speed * Time.fixedDeltaTime * direction);
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
        context.agent.destination = destination;
        return State.Running;
    }

}
