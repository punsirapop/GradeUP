using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SetCollider : ActionNode
{
    public bool isCollider;
    protected override void OnStart() {
        context.boxCollider.enabled = isCollider;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
