using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleStrong : MonoBehaviour
{
    [SerializeField] GameObject ringFloorPrefab;
    
    float scaleRing;
    float durationForTimes;
    int ringCount;
    float spaceRing;

    void Start()
    {
        
    }

    public void Setup(float scaleRing, int ringCount, float spaceRing)
    {
        this.scaleRing = scaleRing;
        this.ringCount = ringCount;
        this.spaceRing = spaceRing;

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
        // for (int i = 0; i < ringCount; i++)
        for (int i = ringCount-1; i >= 0; i--)
        {
            GameObject ringFloor = Instantiate(ringFloorPrefab, transform);
            // ringFloor.GetComponent<RingFloor>().Setup(((scaleRing + 1 + spaceRing) * ringCount) - (scaleRing*(i+1) + spaceRing));
            ringFloor.GetComponent<RingFloor>().Setup(scaleRing + (i*spaceRing));
        }
    }
}
