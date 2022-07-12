using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;
    public characterCon playerController;

    private void Awake()
    {
       if(instance != null) Destroy(this);
       else instance = this;

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<characterCon>();
    }
}
