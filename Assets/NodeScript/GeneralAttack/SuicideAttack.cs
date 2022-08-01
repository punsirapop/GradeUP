using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SuicideAttack : ActionNode
{
    protected override void OnStart() {

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Destroy(context.gameObject);
        
        return State.Success;
    }
}
