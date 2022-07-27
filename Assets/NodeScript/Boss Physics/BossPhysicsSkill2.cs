using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class BossPhysicsSkill2 : ActionNode
{
    public GameObject laserGunPrefab;
    public float duration;
    public int initialLaser;
    float startTime;
    

    protected override void OnStart() {
        startTime = Time.time;
        ShootLaser(blackboard.numberCount);
        Debug.Log(blackboard.numberCount);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Time.time - startTime > duration)
        {
            return State.Success;
        }
        return State.Running;
    }

    private void ShootLaser(int laserNumber)
    {
        for (int i = 0; i < laserNumber + initialLaser; i++)
        {
            GameObject laserGun = Instantiate(laserGunPrefab, new Vector2(0, 0), Quaternion.AngleAxis(i * (360f / (laserNumber + initialLaser)), Vector3.forward));
            Destroy(laserGun, duration);
        }
    }
}
