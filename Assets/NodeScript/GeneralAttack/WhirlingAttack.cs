using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class WhirlingAttack : ActionNode
{
    private bool isSummon;
    private float startTime;
    private List<GameObject> hitboxes;
    public float duration = 10f;
    public int weaponNumber = 4;
    public float attackSpeed = 5f;
    public float weaponRange = 3f;
    public GameObject SwingWeaponPrefab;
    public float swingAngle;


    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (!isSummon)
        {
            isSummon = true;
            Debug.Log(isSummon);
            for (int i = 0; i < weaponNumber; i++)
            {
                Debug.Log("Summon!");
                Quaternion rotation = Quaternion.AngleAxis(i * 360 / weaponNumber, Vector3.forward);

                GameObject hitBox = Instantiate(SwingWeaponPrefab, context.transform.position, rotation);
                hitboxes.Add(hitBox);
                //hitBox.transform.position = new Vector3(context.transform.position.x, context.transform.position.y) + hitBox.transform.rotation *
                //    Quaternion.AngleAxis(swingAngle, Vector3.forward) * new Vector2(weaponRange, 0f);
            }
        }

        foreach (var hitbox in hitboxes)
        {
            hitbox.transform.position = context.transform.position;
        }

        return State.Running;
    }

}