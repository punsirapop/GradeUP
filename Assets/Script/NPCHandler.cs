using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCHandler : MonoBehaviour
{
    [SerializeField] GameObject displayInfo;
    void OnEnable()
    {
        CharacterCon.OnNearNPC += ShowDisplay;
        CharacterCon.OnLeaveNPC += StopDisplay;
    }

    void OnDisable()
    {
        CharacterCon.OnNearNPC += ShowDisplay;
        CharacterCon.OnLeaveNPC += StopDisplay;
    }

    void ShowDisplay(GameObject npc)
    {
        if (gameObject.Equals(npc) && !displayInfo.active)
        {
            displayInfo.SetActive(true);
            CharacterCon.OnInteractNPC += Interact;
        }
    }

    void StopDisplay(GameObject npc)
    {
        if (gameObject.Equals(npc) && displayInfo.active)
        {
            displayInfo.SetActive(false);
            CharacterCon.OnInteractNPC -= Interact;
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
