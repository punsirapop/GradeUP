using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;
using UnityEngine.AI;

public class BossChemUltimate : ActionNode
{
    [SerializeField] GameObject poisonFillPrefab;
    [SerializeField] Vector2 spawnUltimatePosition;
    [SerializeField] Vector2 spawnUltimateScale;
    [SerializeField] float ultimateDuration = 10f;

    float startTime;
    GameObject poisonFill;

    protected override void OnStart() {

        // MoveToPosition();
        SpawnPoisonFill();
        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // if (FindObjectOfType<PoisonFill>() != null) return State.Success;
        // Debug.Log($"{context.transform.position} and {spawnUltimatePosition}");
        // if (Vector2.Distance(context.transform.position, spawnUltimatePosition) <= context.agent.stoppingDistance)
        // {
            // SpawnPoisonFill();
            return State.Success;
        // }
        // return State.Running;
    }

    private void MoveToPosition()
    {
        context.agent.SetDestination(spawnUltimatePosition);
    }

    private void SpawnPoisonFill()
    {
        poisonFill = Instantiate(poisonFillPrefab, spawnUltimatePosition, Quaternion.identity);
        poisonFill.transform.localScale = spawnUltimateScale;
        context.gameObject.GetComponent<EnemyController>().SetVisible(false, ultimateDuration);
        Destroy(poisonFill, ultimateDuration);
    }
}
