using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCHandler : MonoBehaviour
{
    [SerializeField] GameObject displayInfo;
    void OnEnable()
    {
        characterCon.OnNearNPC += ShowDisplay;
        characterCon.OnLeaveNPC += StopDisplay;
    }

    void OnDisable()
    {
        characterCon.OnNearNPC += ShowDisplay;
        characterCon.OnLeaveNPC += StopDisplay;
    }

    void ShowDisplay(GameObject npc)
    {
        if (gameObject.Equals(npc) && !displayInfo.active)
        {
            displayInfo.SetActive(true);
            characterCon.OnInteractNPC += Interact;
        }
    }

    void StopDisplay(GameObject npc)
    {
        if (gameObject.Equals(npc) && displayInfo.active)
        {
            displayInfo.SetActive(false);
            characterCon.OnInteractNPC -= Interact;
        }
    }

    void Interact()
    {
        if (displayInfo.active)
        {
            Debug.Log("Talking with " + name);
        }
    }
}
