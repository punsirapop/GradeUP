using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossLngNormal : ActionNode
{
    public SpeakerLouds spawnSpeakerLouds;
    public float skillDuration = 5f;

    float startTime;

    protected override void OnStart() {
        SpeakerLoudAttack();
        startTime = Time.time;
    }

    private void SpeakerLoudAttack()
    {
        spawnSpeakerLouds = FindObjectOfType<SpeakerLouds>();

        foreach (Component child in spawnSpeakerLouds.transform)
        {
            child.GetComponent<SpeakerLoud>().Attack(skillDuration);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > skillDuration)
        {
            return State.Success;
        }
        return State.Running;
    }
}
