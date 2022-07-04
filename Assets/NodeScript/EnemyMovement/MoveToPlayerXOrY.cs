using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToPlayerXOrY : ActionNode
{
    public float speed = 5;
    public float duration = 1;
    private float startTime;
    public float stoppingDistance;
    private Transform target;
    
    public bool moveXAxis;
    public bool moveYAxis;
    public bool moveBothAxis;
    public bool moveShortestAxis;

    private float distanceX;
    private float distanceY;

    public Vector2 direction;

    protected override void OnStart()
    {
        target = MainGame.instance.playerController.transform;
        startTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        distanceX = target.position.x - context.agent.transform.position.x;
        distanceY = target.position.y - context.agent.transform.position.y;

        if (Time.time - startTime >= duration)
        {
            return State.Success;
        }

        
        
        if (moveXAxis)
        {
            if (Mathf.Abs(distanceX) < stoppingDistance)
            {
                return State.Success;
            }

            MoveToPlayerXAxis();
            return State.Running;
        }

        if (moveYAxis)
        {
            if (Mathf.Abs(distanceY) < stoppingDistance)
            {
                Debug.Log("stop distanceY");
                return State.Success;
            }
            //Debug.Log("moveYAxis!");

            MoveToPlayerYAxis();
            return State.Running;
        }

        if (moveBothAxis)
        {
            if (Mathf.Abs(distanceX) < stoppingDistance || Mathf.Abs(distanceY) < stoppingDistance)
            {
                return State.Success;
            }

            direction = (new Vector2( Mathf.Sign(distanceX) * 1f, Mathf.Sign(distanceY) * 1f));
            direction.Normalize();
            context.physics.MovePosition(context.physics.position + speed * Time.fixedDeltaTime * direction);
            return State.Running;
        }

        if (moveShortestAxis)
        {
            if(Mathf.Abs(distanceX) < Mathf.Abs(distanceY))
            {
                if (Mathf.Abs(distanceX) < stoppingDistance)
                {
                    return State.Success;
                }
                MoveToPlayerXAxis();
                return State.Running;
            }
            else
            {
                if (Mathf.Abs(distanceY) < stoppingDistance)
                {
                    return State.Success;
                }
                MoveToPlayerYAxis();
                return State.Running;
            }
        }
        return State.Running;
    }

    private void MoveToPlayerXAxis()
    {
        direction = (new Vector2(Mathf.Sign(distanceX) * 1f, 0f));
        context.physics.MovePosition(context.physics.position + speed * Time.fixedDeltaTime * direction);
    }

    private void MoveToPlayerYAxis()
    {
        // if x = 0 somehow not worked //
        direction = (new Vector2(0.0001f, Mathf.Sign(distanceY) * 1f));
        Debug.Log("Pieng ="+ speed * Time.fixedDeltaTime * direction);
        context.physics.MovePosition(context.physics.position + speed * Time.fixedDeltaTime * direction);
    }

}
