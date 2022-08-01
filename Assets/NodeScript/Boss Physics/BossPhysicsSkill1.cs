using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class BossPhysicsSkill1 : ActionNode
{
    public string objectTag;
    public GameObject[] objects;
    
    public GameObject plasmaFieldPrefab;
    public float duration;
    float startTime;

    protected override void OnStart() {
        startTime = Time.time;
        objects = GameObject.FindGameObjectsWithTag(objectTag);

        foreach (var obj in objects)
        {
            GameObject plasmaField = Instantiate(plasmaFieldPrefab, obj.transform.position, Quaternion.identity);
            Destroy(plasmaField, duration);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > duration)
        {
            return State.Success;
        }
        return State.Running;
    }
}
