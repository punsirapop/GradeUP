using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFight : MonoBehaviour
{
    public GameObject changeRoom;
    public CharacterCon character;
    void Start()
    {
        changeRoom.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //testing class type
            character = collision.GetComponent<CharacterCon>();
            Debug.Log(character);


            int playerClassIndex = PlayerPrefs.GetInt("playerClass", 0);
            Debug.Log("Player Class Index is " + playerClassIndex);
            changeRoom.SetActive(true);
        }
    }
}
