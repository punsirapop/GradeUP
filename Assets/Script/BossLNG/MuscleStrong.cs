using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleStrong : MonoBehaviour
{
    [SerializeField] GameObject ringFloorPrefab;
    
    float durationForTimes;
    float attackTimes;

    void Start()
    {
        
    }

    public void Setup(float attackTimes, float durationForTimes)
    {
        this.attackTimes = attackTimes;
        this.durationForTimes = durationForTimes;

        // Debug.Log(durationForTimes);
        StartCoroutine(SpawnRingsFloor());
    }

    IEnumerator SpawnRingsFloor()
    {
        for (int i = 0; i < attackTimes; i++)
        {
            GameObject ringFloor = Instantiate(ringFloorPrefab, transform);
            ringFloor.GetComponent<RingFloor>().Setup(durationForTimes, i);
            yield return new WaitForSeconds(durationForTimes / 3);
            Destroy(ringFloor.gameObject,  (durationForTimes / 3) * (attackTimes - i));
        }
    }
}
