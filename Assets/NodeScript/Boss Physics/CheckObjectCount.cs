using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckObjectCount : ActionNode
{
    public string objectTag;
    public GameObject[] objects;

    protected override void OnStart()
    {
        blackboard.positions.Clear();
        objects = GameObject.FindGameObjectsWithTag(objectTag);
        blackboard.numberCount = objects.Length;

        foreach (var obj in objects)
        {
            blackboard.positions.Add(obj.transform.position);
            Debug.Log("weapon = " + obj.transform.position);
        }

        Debug.Log("weaponCount = " + blackboard.numberCount);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
