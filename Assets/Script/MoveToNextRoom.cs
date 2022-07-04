using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToNextRoom : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    private Vector2 moveToPosition;
    private Vector3 cameraPosition;

    public characterCon playerController;
    public GameObject player;
    public new Transform camera;
    public LevelLoader levelLoader;

    public static bool playerMoveable = true;

    private void Awake()
    {
        playerController = player.GetComponent<characterCon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        playerMoveable = false;
        levelLoader.LoadNextLevel();
        yield return new WaitForSeconds(1.5f);
        playerMoveable = true;

        int randomXPosition = Random.Range(1, 4);
        switch (randomXPosition)
        {
            case 1:
                xPosition = -2;
                break;
            case 2:
                xPosition = 0;
                break;
            case 3:
                xPosition = 2;
                break;
        }

        int randomYPosition = Random.Range(1, 4);
        switch (randomYPosition)
        {
            case 1:
                yPosition = 0;
                break;
            case 2:
                yPosition = -13;
                break;
            case 3:
                yPosition = -26;
                break;
        }
        moveToPosition = new Vector2(xPosition, yPosition);
        cameraPosition = new Vector3(0, yPosition, -10);

        playerController.transform.position = moveToPosition;
        camera.position = cameraPosition;

        gameObject.SetActive(false);
    }
}
