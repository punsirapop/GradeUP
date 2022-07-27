using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class PELockPlayer : ActionNode
{
    private Transform target;
    public GameObject goalLinePrefab;
    protected override void OnStart() {
        target = MainGame.instance.playerController.transform;
        blackboard.moveToPosition = target.position;
        GameObject goalLine = Instantiate(goalLinePrefab, target.position, Quaternion.identity);
        Destroy(goalLine, 1.5f);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
