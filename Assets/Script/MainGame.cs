using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;
    public GameObject player;
    public StatusManager statusManager;
    public CharacterCon playerController;

    private void Awake()
    {
        if(instance != null) Destroy(this);
        else instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        statusManager = player.GetComponent<StatusManager>();
        playerController = player.GetComponent<CharacterCon>();
    }
}
