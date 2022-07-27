using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class BossPESkill2 : ActionNode
{
    public GameObject elitePEPrefab;
    public Vector2 position1;
    public Vector2 position2;

    protected override void OnStart() {
        GameObject enemyPE1 = Instantiate(elitePEPrefab, position1, Quaternion.identity);
        GameObject enemyPE2 = Instantiate(elitePEPrefab, position2, Quaternion.identity);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
