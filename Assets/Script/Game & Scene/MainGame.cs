using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;
    public CharacterCon playerController;
    public SpawnPlayer spawnPlayer;

    [SerializeField] GameObject enemySet;
    [SerializeField] GameObject reward;
    [SerializeField] GameObject door;

    //public SaveManager saveManager;
    void Awake()
    {
        if(instance != null) Destroy(this);
        else instance = this;
        spawnPlayer.SpawnPlayerClass();
        playerController = FindObjectOfType<CharacterCon>();

        enemySet.SetActive(true);
        enemySet.GetComponent<CheckChildrenObjectNumber>().OnRoomCleared += RoomCleared;
    }

    private void OnDisable()
    {
        enemySet.GetComponent<CheckChildrenObjectNumber>().OnRoomCleared -= RoomCleared;
    }

    void RoomCleared()
    {
        Debug.Log("Room Cleared!");
        door.SetActive(true);
    }
}
