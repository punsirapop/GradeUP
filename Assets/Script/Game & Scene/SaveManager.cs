using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //-----player-----//
    public PlayerController player;

    //Player Stat
    public float currentPlayerHealth;

    //Player Reward-Stat
    public float physicsReward;
    public float chemReward;
    public float pEReward;
    public float lNGReward;
    public float artsReward;

    //Player Class
    public int playerClass;
    public int playerSubclass;

    //Player Current Grade
    public float physicsGrade;
    public float chemGrade;
    public float pEGrade;
    public float lNGGrade;
    public float artsGrade;

    //Character animation
    public int hairIndex;
    public int eyesIndex;
    public int mouthIndex;

    //Player Trait
    public int trait01;
    public int trait02;
    public int trait03;

    //Player Item
    public int activeItem1;
    public int item2;
    public int item3;
    public int item4;
    public int item5;
    public int bottleStack;

    //Player Gold
    public int gold;

    //-----scene-----//
    public int roomType;
    public int roomNumber;

    public int isMetHealingRoom;
    public int isMetShopRoom;
    public int isMetEventRoom;

    public int roomSize;
    public int roomPreset;
    public int enemyPreset;

    public int roomReward;
    public int NextRoomReward1;
    public int NextRoomReward2;

    public void Init()
    {
        LoadGame();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SaveGame();
        }    
    }
    public void SaveGame()
    {
        //-----player-----//
        player = GameObject.FindObjectOfType<PlayerController>();
        currentPlayerHealth = player.currentHP;
        Debug.Log("currentPlayerHealth = " + currentPlayerHealth);
        //Player Stat
        //PlayerPrefs.SetFloat("currentPlayerHealth", currentPlayerHealth);

        ////Player Reward-Stat
        //PlayerPrefs.SetFloat("physicsReward", physicsReward);
        //PlayerPrefs.SetFloat("chemReward", chemReward);
        //PlayerPrefs.SetFloat("pEReward", pEReward);
        //PlayerPrefs.SetFloat("lNGReward", lNGReward);
        //PlayerPrefs.SetFloat("artsReward", artsReward);

        ////Player Class
        //PlayerPrefs.SetInt("playerClass", playerClass);
        //PlayerPrefs.SetInt("playerSubclass", playerSubclass);

        ////Player Current Grade
        //PlayerPrefs.SetFloat("physicsGrade", physicsGrade);
        //PlayerPrefs.SetFloat("chemGrade", chemGrade);
        //PlayerPrefs.SetFloat("pEGrade", pEGrade);
        //PlayerPrefs.SetFloat("lNGGrade", lNGGrade);
        //PlayerPrefs.SetFloat("artsGrade", artsGrade);

        ////Character animation
        //PlayerPrefs.SetInt("hairIndex", hairIndex);
        //PlayerPrefs.SetInt("eyesIndex", eyesIndex);
        //PlayerPrefs.SetInt("mouthIndex", mouthIndex);

        ////Player Trait
        //PlayerPrefs.SetInt("trait01", trait01);
        //PlayerPrefs.SetInt("trait02", trait02);
        //PlayerPrefs.SetInt("trait03", trait03);

        ////Player Item
        //PlayerPrefs.SetInt("activeItem1", activeItem1);
        //PlayerPrefs.SetInt("item2", item2);
        //PlayerPrefs.SetInt("item3", item3);
        //PlayerPrefs.SetInt("item4", item4);
        //PlayerPrefs.SetInt("item5", item5);
        //PlayerPrefs.SetInt("bottleStack", bottleStack);

        ////Player Gold
        //PlayerPrefs.SetInt("gold", gold);

        ////-----scene-----//
        //PlayerPrefs.SetInt("roomType", roomType);
        //PlayerPrefs.SetInt("roomNumber", roomNumber);

        //PlayerPrefs.SetInt("isMetHealingRoom", isMetHealingRoom);
        //PlayerPrefs.SetInt("isMetShopRoom", isMetShopRoom);
        //PlayerPrefs.SetInt("isMetEventRoom", isMetEventRoom);

        //PlayerPrefs.SetInt("roomSize", roomSize);
        //PlayerPrefs.SetInt("roomPreset", roomPreset);
        //PlayerPrefs.SetInt("enemyPreset", enemyPreset);

        //PlayerPrefs.SetInt("roomReward", roomReward);
        //PlayerPrefs.SetInt("NextRoomReward1", NextRoomReward1);
        //PlayerPrefs.SetInt("NextRoomReward2", NextRoomReward2);
        PlayerPrefs.Save();

    }

    public void LoadGame()
    {
        //-----player-----//

        //Player Stat
        player = GameObject.FindObjectOfType<PlayerController>();
        currentPlayerHealth = PlayerPrefs.GetFloat("currentPlayerHealth", 100);
        player.currentHP = currentPlayerHealth;

        ////Player Reward-Stat
        //physicsReward = PlayerPrefs.GetFloat("physicsReward", 50f);
        //chemReward = PlayerPrefs.GetFloat("chemReward", 50f);
        //pEReward = PlayerPrefs.GetFloat("pEReward", 50f);
        //lNGReward = PlayerPrefs.GetFloat("lNGReward", 50f);
        //artsReward = PlayerPrefs.GetFloat("artsReward", 50f);

        ////Player Class
        //playerClass = PlayerPrefs.GetInt("playerClass", 1);
        //playerSubclass = PlayerPrefs.GetInt("playerSubclass", 0);

        ////Player Current Grade
        //physicsGrade = PlayerPrefs.GetFloat("physicsGrade", 0);
        //chemGrade = PlayerPrefs.GetFloat("chemGrade", 0);
        //pEGrade = PlayerPrefs.GetFloat("pEGrade", 0);
        //lNGGrade = PlayerPrefs.GetFloat("lNGGrade", 0);
        //artsGrade = PlayerPrefs.GetFloat("artsGrade", 0);

        ////Character animation
        //hairIndex = PlayerPrefs.GetInt("hairIndex", 0);
        //eyesIndex = PlayerPrefs.GetInt("eyesIndex", 0);
        //mouthIndex = PlayerPrefs.GetInt("mouthIndex", 0);

        ////Player Trait
        //trait01 = PlayerPrefs.GetInt("trait01", 1);
        //trait02 = PlayerPrefs.GetInt("trait02", 0);
        //trait03 = PlayerPrefs.GetInt("trait03", 0);

        ////Player Item
        //activeItem1 = PlayerPrefs.GetInt("activeItem1", 0);
        //item2 = PlayerPrefs.GetInt("item2", 0);
        //item3 = PlayerPrefs.GetInt("item3", 0);
        //item4 = PlayerPrefs.GetInt("item4", 0);
        //item5 = PlayerPrefs.GetInt("item5", 0);
        //bottleStack = PlayerPrefs.GetInt("bottleStack", 3);

        ////Player Gold
        //gold = PlayerPrefs.GetInt("gold", 0);

        ////-----scene-----//
        //roomType = PlayerPrefs.GetInt("roomType", 0);
        //roomNumber = PlayerPrefs.GetInt("roomNumber", 0);

        //isMetHealingRoom = PlayerPrefs.GetInt("isMetHealingRoom", 0);
        //isMetShopRoom = PlayerPrefs.GetInt("isMetShopRoom", 0);
        //isMetEventRoom = PlayerPrefs.GetInt("isMetEventRoom", 0);

        //roomSize = PlayerPrefs.GetInt("roomSize", 0);
        //roomPreset = PlayerPrefs.GetInt("roomPreset", 0);
        //enemyPreset = PlayerPrefs.GetInt("enemyPreset", 0);

        //roomReward = PlayerPrefs.GetInt("roomReward", 0);
        //NextRoomReward1 = PlayerPrefs.GetInt("NextRoomReward1", 1);
        //NextRoomReward2 = PlayerPrefs.GetInt("NextRoomReward2", 2);

    }
}
