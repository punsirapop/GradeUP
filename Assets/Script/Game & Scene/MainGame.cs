using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainGame : MonoBehaviour
{
    public static MainGame instance;
    public CharacterCon playerController;
    public SpawnPlayer spawnPlayer;

    [SerializeField] List<GameObject> rooms;
    [SerializeField] EnemySetHolder enemies;
    [SerializeField] GameObject reward;
    [SerializeField] GameObject door;
    [SerializeField] NavMeshSurface2d surface;
    [SerializeField] GameObject EnemyHolder;

    int roomLayoutIndex, enemyLayoutIndex;
    SpawnerManager spawnerManager;
    List<GameObject> enemySet;
    List<Transform> enemyPos;
    GameObject enemySetObject;
    //public SaveManager saveManager;
    void Awake()
    {
        if(instance != null) Destroy(this);
        else instance = this;

        // get room&enemy layout
        roomLayoutIndex = Random.Range(0, rooms.Count);
        enemyLayoutIndex = Random.Range(0, enemies.enemySetSOs.Count);
        Debug.Log("RoomLayout: " + roomLayoutIndex + "\nEnemyLayout: " + enemyLayoutIndex);
        spawnerManager = rooms[roomLayoutIndex].GetComponent<SpawnerManager>();
        enemySet = new List<GameObject>(enemies.enemySetSOs[enemyLayoutIndex].EnemySets);
        enemyPos = new List<Transform>(spawnerManager.EnemySpawners);
        
        // spawn player
        rooms[roomLayoutIndex].SetActive(true);
        surface.BuildNavMesh();
        spawnPlayer.SpawnPlayerClass(spawnerManager.PlayerSpawner.position);
        playerController = FindObjectOfType<CharacterCon>();

        // spawn enemy
        enemySetObject = new GameObject("Enemy Set");
        while(enemySet.Count > 0)
        {
            int i = Random.Range(0, enemySet.Count);
            Instantiate(enemySet[i], enemyPos[i].position, 
                transform.rotation, enemySetObject.transform);
            enemyPos.RemoveAt(i);
            enemySet.RemoveAt(i);
        }
        Debug.Log("Enemy spawned: " + enemySetObject.transform.childCount);
        enemySetObject.AddComponent<CheckChildrenObjectNumber>();
        enemySetObject.GetComponent<CheckChildrenObjectNumber>().OnRoomCleared += RoomCleared;
    }

    private void OnDisable()
    {
        enemySetObject.GetComponent<CheckChildrenObjectNumber>().OnRoomCleared -= RoomCleared;
    }

    void RoomCleared()
    {
        Debug.Log("Room Cleared!");
        door.SetActive(true);
    }
}
