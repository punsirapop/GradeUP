using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class TeleportToPosition : ActionNode
{
    private int randomTeleport;

    protected override void OnStart() {
        
        if(blackboard.numberCount != 0)
        {
            randomTeleport = Random.Range(0, blackboard.numberCount);
            context.transform.position = blackboard.positions[randomTeleport];
        }
        Debug.Log("teleport to " + randomTeleport);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
