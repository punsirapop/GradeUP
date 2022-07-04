using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveInDirection : ActionNode
{
    public float speed = 5;
    public float duration = 1;
    float startTime;
    public bool isRandomDirection;

    public Vector2 direction;

    protected override void OnStart()
    {
        if (isRandomDirection)
        {
            int Randomdirection = Random.Range(1, 5);

            switch (Randomdirection)
            {
                case 1:
                    Debug.Log("Right");
                    direction = new Vector2(1, 0);
                    break;
                case 2:
                    Debug.Log("Left");
                    direction = new Vector2(-1, 0);
                    break;
                case 3:
                    Debug.Log("Top");
                    direction = new Vector2(0, 1);
                    break;
                case 4:
                    Debug.Log("Down");
                    direction = new Vector2(0, -1);
                    break;
            }
        }



        direction.Normalize();
        // if x = 0 somehow not worked //
        direction.x += 0.0001f;
        startTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {   
        if (Time.time - startTime >= duration)
        {
            return State.Success;
        }
        context.physics.MovePosition(context.physics.position + speed * Time.fixedDeltaTime * direction);
        return State.Running;
    }
}
