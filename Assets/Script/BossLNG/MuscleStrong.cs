using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleStrong : MonoBehaviour
{
    [SerializeField] GameObject ringFloorPrefab;
    
    float scaleRing;
    float durationForTimes;
    float ringCount;

    void Start()
    {
        
    }

    public void Setup(float scaleRing, float ringCount, float durationForTimes)
    {
        this.scaleRing = scaleRing;
        this.ringCount = ringCount;
        this.durationForTimes = durationForTimes;

        // Debug.Log(durationForTimes);
        SpawnRingsFloor();
        // StartCoroutine(SpawnRingsFloor());
    }

    // IEnumerator SpawnRingsFloor()
    // {
    //     for (int i = 0; i < attackTimes; i++)
    //     {
    //         GameObject ringFloor = Instantiate(ringFloorPrefab, transform);
    //         ringFloor.GetComponent<RingFloor>().Setup(durationForTimes, i);
    //         yield return new WaitForSeconds(durationForTimes / 3);
    //         Destroy(ringFloor.gameObject,  (durationForTimes / 3) * (attackTimes - i));
    //     }
    // }

    void SpawnRingsFloor()
    {
        for (int i = 0; i < ringCount; i++)
        {
            GameObject ringFloor = Instantiate(ringFloorPrefab, transform);
            ringFloor.GetComponent<RingFloor>().Setup(((scaleRing + 1) * ringCount) - (scaleRing*(i+1)), durationForTimes);
        }
    }
}
