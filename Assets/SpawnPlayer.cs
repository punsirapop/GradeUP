using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPlayer : MonoBehaviour
{
    private int playerClassIndex;
    public GameObject playerPhysics;
    public GameObject playerChem;
    public GameObject playerPE;
    public GameObject playerLng;
    public GameObject playerArts;

    public float playerCurrentHp;

    public static event Action<float> SetCurrentHp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SpawnPlayerClass()
    {
        playerClassIndex = PlayerPrefs.GetInt("playerClass", 0);
        playerCurrentHp = PlayerPrefs.GetFloat("playerCurrentHp", 50);
        Debug.Log("player Current Hp From Save = " + playerCurrentHp);
        switch (playerClassIndex)
        {
            case 0:
                _ = Instantiate(playerPhysics, transform.position, Quaternion.identity);
                break;
            case 1:
                _ = Instantiate(playerChem, transform.position, Quaternion.identity);
                break;
            case 2:
                _ = Instantiate(playerPE, transform.position, Quaternion.identity);
                break;
            case 3:
                _ = Instantiate(playerLng, transform.position, Quaternion.identity);
                break;
            case 4:
                _ = Instantiate(playerArts, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
        Debug.Log("HP Invoker");
        SetCurrentHp?.Invoke(playerCurrentHp);
    }
}
