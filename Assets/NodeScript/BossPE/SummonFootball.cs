using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SummonFootball : ActionNode
{
    public GameObject footballPrefab;
    public List<Vector2> positions;
    public float ballDuration;
    private int footballCount;
    
    protected override void OnStart() {
        footballCount = positions.Count;
        for (int i = 0; i < footballCount; i++)
        {
            GameObject football = Instantiate(footballPrefab, positions[i], Quaternion.identity);
            Destroy(football, ballDuration);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
