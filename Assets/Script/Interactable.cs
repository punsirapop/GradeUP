using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected BoxCollider2D boxCollider;
    private bool isInteract = false;

    protected virtual void OnUse()
    {
        if (this.isInteract == false)
        {

        }
        else
        {

        }
        //when player used
    }
}
