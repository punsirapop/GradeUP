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
        ClearPoisonPool();
        SpawnPoisonFill();
        startTime = Time.time;
    }

    private void ClearPoisonPool()
    {
        PoisonPool[] pools = FindObjectsOfType<PoisonPool>();
        foreach (PoisonPool pool in pools)
        {
            Destroy(pool);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // if (FindObjectOfType<PoisonFill>() != null) return State.Success;

        // if ((Vector2) context.transform.position == spawnUltimatePosition)
        // {
        //     SpawnPoisonFill();
        //     return State.Success;
        // }
        return State.Success;
    }

    private void MoveToPosition()
    {
        context.gameObject.GetComponent<NavMeshAgent>().SetDestination(spawnUltimatePosition);
    }

    private void SpawnPoisonFill()
    {
        poisonFill = Instantiate(poisonFillPrefab, spawnUltimatePosition, Quaternion.identity);
        poisonFill.transform.localScale = spawnUltimateScale;
        context.gameObject.GetComponent<EnemyController>().SetVisible(false, ultimateDuration);
        Destroy(poisonFill, ultimateDuration);
    }
}
