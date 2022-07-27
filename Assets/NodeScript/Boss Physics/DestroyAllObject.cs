using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class DestroyAllObject : ActionNode
{
    public string objectTag;
    public GameObject[] objects;
    public float delay;
    protected override void OnStart() {
        objects = GameObject.FindGameObjectsWithTag(objectTag);
        foreach (var obj in objects)
        {
            Destroy(obj, delay);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
