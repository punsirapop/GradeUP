using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 target;

    public bool isLargeRoom;
    private float positionX;
    private float positionY;

    void Update()
    {
        target = MainGame.instance.playerController.transform.position;

        //-----Large Room-----//
        if (isLargeRoom)
        {
            //get PositionY
            if (target.y <= -3.75f)
            {
                positionY = -3.75f;
            }
            else if (target.y >= 3.75f)
            {
                positionY = 3.75f;
            }
            else
            {
                positionY = target.y;
            }
            // get PositionX
            if (target.x <= -6.7f)
            {
                positionX = -6.7f;
            }
            else if (target.x >= 6.7f)
            {
                positionX = 6.7f;
            }
            else
            {
                positionX = target.x;
            }
            transform.position = new Vector3(positionX, positionY, transform.position.z);
        }
        //-----Medium Room-----//
        else
        {
            if (target.x <= -13.35f)
            {
                transform.position = new Vector3(-13.35f, transform.position.y, transform.position.z);
            }
            else if (target.x >= 13.35f)
            {
                transform.position = new Vector3(13.35f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(target.x, transform.position.y, transform.position.z);
            }
        }
    }
}
