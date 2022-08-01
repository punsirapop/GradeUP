using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;
    public CharacterCon playerController;
    public SpawnPlayer spawnPlayer;
    //public SaveManager saveManager;
    private void Awake()
    {
        if(instance != null) Destroy(this);
        else instance = this;
        spawnPlayer.SpawnPlayerClass();
        playerController = FindObjectOfType<CharacterCon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //saveManager.Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
