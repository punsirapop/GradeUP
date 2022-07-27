using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextRoom : MonoBehaviour
{
    public enum RoomType
    {
        smallRoom,
        mediumRoom,
        largeRoom,
        bossRoom,
    }

    [Header("Next Room")]
    public RoomType roomType;
    public int nextRoomIndex; 
    private string roomSize;
    private string nextRoom;


    //public PlayerController playerController;
    //public GameObject player;
    //public LevelLoader levelLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Check Normal Room Type
            switch (roomType)
            {
                case RoomType.smallRoom:
                    roomSize = "Small Room ";
                    break;
                case RoomType.mediumRoom:
                    roomSize = "Medium Room ";
                    break;
                case RoomType.largeRoom:
                    roomSize = "Large Room ";
                    break;
                case RoomType.bossRoom:
                    roomSize = "Boss Room ";
                    break;
                default:
                    roomSize = "Small Room ";
                    break;
            }

            //Check Boss room tpye
            if(roomType == RoomType.bossRoom)
            {
                switch (nextRoomIndex)
                {
                    case 1:
                        nextRoom = "Boss Physics Room";
                        break;
                    case 2:
                        nextRoom = "Boss Chem Room";
                        break;
                    case 3:
                        nextRoom = "Boss PE Room";
                        break;
                    case 4:
                        nextRoom = "Boss LNG Room";
                        break;
                    case 5:
                        nextRoom = "Final Boss Room";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                nextRoom = string.Concat(roomSize, nextRoomIndex);
            }
            SaveCurrentHp(collision.gameObject);


            SceneManager.LoadScene(nextRoom);
        }
    }

    private void SaveCurrentHp(GameObject player)
    {
        float playerHp = player.GetComponent<PlayerHealth>().GetHp();
        PlayerPrefs.SetFloat("playerCurrentHp", playerHp);
    }
}
