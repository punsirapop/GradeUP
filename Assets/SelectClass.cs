using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectClass : MonoBehaviour
{
    public GameObject playerClassPrefab;
    // Start is called before the first frame update

    public enum PlayerClass
    {
        physics,
        chem,
        pe,
        lng,
        arts
    }
    public PlayerClass playerClass;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangePlayerClass(collision.gameObject);
            SaveCurrentClass();   
        }
    }

    private void ChangePlayerClass(GameObject player)
    {
        Vector2 playerPos = player.transform.position;
        Destroy(player.gameObject);
        GameObject newPlayerClass = Instantiate(playerClassPrefab, playerPos + new Vector2(0, 3), Quaternion.identity);
        Debug.Log("My class is " + playerClass);
    }

    private void SaveCurrentClass()
    {
        switch (playerClass)
        {
            case PlayerClass.physics:
                PlayerPrefs.SetInt("playerClass", 0);
                break;
            case PlayerClass.chem:
                PlayerPrefs.SetInt("playerClass", 1);
                break;
            case PlayerClass.pe:
                PlayerPrefs.SetInt("playerClass", 2);
                break;
            case PlayerClass.lng:
                PlayerPrefs.SetInt("playerClass", 3);
                break;
            case PlayerClass.arts:
                PlayerPrefs.SetInt("playerClass", 4);
                break;
            default:
                break;
        }

    }
}
