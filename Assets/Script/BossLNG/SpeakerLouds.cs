using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerLouds : MonoBehaviour
{
    [SerializeField] GameObject speakerLoudPrefab;
    [SerializeField] float speakerLoudSpeed = 5f;

    [SerializeField] Vector2[] patrolPoints = new Vector2[4];

    // Start is called before the first frame update
    void Start()
    {
        SpawnSpeakerLounds();
    }

    private void SpawnSpeakerLounds()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject speakLound = Instantiate(speakerLoudPrefab, patrolPoints[i], Quaternion.identity, transform);

            Vector2[] newPatrolPoints = new Vector2[4];
            for (int j = 0; j < 4; j++)
            {
                newPatrolPoints[j] = patrolPoints[(i+j+1) % 4];
            }

            speakLound.GetComponent<SpeakerLoud>().Setup(newPatrolPoints, speakerLoudSpeed);
        }
    }
}
