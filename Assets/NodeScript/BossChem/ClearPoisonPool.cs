using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ClearPoisonPool : ActionNode
{
    bool isSuccess;

    protected override void OnStart() {
        ClearPoisonPools();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (isSuccess)
        {
            return State.Success;
        }
        return State.Running;
    }

    private void ClearPoisonPools()
    {
        PoisonPool[] pools = FindObjectsOfType<PoisonPool>();
        foreach (PoisonPool pool in pools)
        {
            Destroy(pool.gameObject);
        }
        isSuccess = true;
    }
}
