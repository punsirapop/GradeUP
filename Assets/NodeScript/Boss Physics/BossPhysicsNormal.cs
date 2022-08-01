using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class BossPhysicsNormal : ActionNode
{
    public GameObject markerPrefab;
    private GameObject marker;

    public Vector3 direction;

    public float posXMin;
    public float posXMax;
    public float posYMin;
    public float posYMax;

    private float positionX;
    private float positionY;


    public float speed = 5;

    protected override void OnStart()
    {
        positionX = Random.Range(posXMin, posXMax);
        positionY = Random.Range(posYMin, posYMax);
        Debug.Log("position = " + positionX + ", " + positionY);
        blackboard.moveToPosition = new Vector2(positionX, positionY);
        direction = (blackboard.moveToPosition - new Vector2(context.transform.position.x, context.transform.position.y)).normalized;

        marker = Instantiate(markerPrefab, context.transform.position, Quaternion.identity);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(Vector2.Distance(new Vector2(positionX, positionY), marker.transform.position) < speed * 0.02f)
        {
            return State.Success;

        }
        //if (Mathf.Abs(positionX - marker.transform.position.x) < speed * 0.02f && Mathf.Abs(positionY - marker.transform.position.y) < speed * 0.02f)
        //{
        //}
        marker.transform.position = marker.transform.position + speed * Time.fixedDeltaTime * direction;
        return State.Running;
    }
}
