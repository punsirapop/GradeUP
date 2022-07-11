using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossChemSkill2 : ActionNode
{
    [SerializeField] GameObject poisonExplodeVFXPrefab;

    [SerializeField] float skillDuration = 1f;

    PlayerController player;

    bool isPlayerPoisonning;

    float startTime;

    protected override void OnStart() {
        player = MainGame.instance.playerController;
        isPlayerPoisonning = player.poisonLevel != 0;

        if (isPlayerPoisonning)
        {
            PoisonExplode();
        }
        startTime = Time.time;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (!isPlayerPoisonning || Time.time - startTime > skillDuration)
        {
            return State.Success;
        }
        return State.Running;
    }

    private void PoisonExplode()
    {
        Debug.Log("Boom!");
        GameObject vfx = Instantiate(poisonExplodeVFXPrefab, player.transform.position, Quaternion.identity);
        player.ResetPoisonLevel();
        Destroy(vfx, skillDuration);
    }

}
