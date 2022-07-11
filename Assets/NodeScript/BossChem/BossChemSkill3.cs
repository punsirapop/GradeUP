using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossChemSkill3 : ActionNode
{
    [SerializeField] float invisibleDuration = 3f;

    GameObject enemy;
    bool isVisible;
    bool isSuccess;

    float startTime;

    protected override void OnStart() {
        enemy = context.gameObject;

        // SetVisible(isVisible);
        // Invisible();
        startTime = Time.time;
    }
    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // if (isSuccess)
        // {
        //     isSuccess = false;
        //     return State.Success;
        // }
        return State.Success;
        // return State.Running;
    }

    private void Invisible()
    {
        // PoisonPool[] pools = FindObjectsOfType<PoisonPool>();
        // if (pools.Length != 0)
        // {
        //     int rdmIndex = new System.Random().Next(pools.Length);
        //     Debug.Log($"rdm : {pools[rdmIndex].name}");
            // blackboard.moveToPosition = pools[rdmIndex].transform.position;
        // };
        // isSuccess = true;
    }


    // private void SetVisible(bool visible)
    // {
    //     foreach (Component child in enemy.transform)
    //     {
    //         SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
    //         if (sprite != null)
    //         {
    //             sprite.enabled = visible;
    //         }
    //     }
    // }
}
