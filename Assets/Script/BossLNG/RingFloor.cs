using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFloor : MonoBehaviour
{
    [SerializeField] float scaleUp = 10f;

    public void Setup(float ringScale)
    {
        // if (index%2 == 0)
        // {
        //     GetComponentInChildren<SpriteRenderer>().color = Color.red;
        // }
        // else
        // {
        //     GetComponentInChildren<SpriteRenderer>().color = Color.green;
        // }
        // Debug.Log(ringScale);
        transform.localScale = new Vector2(ringScale, ringScale);
        // StartCoroutine(ScaleUp());
    }

    // IEnumerator ScaleUp()
    // {
    //     Vector2 startScale = transform.localScale;
    //     Vector2 endScale = new Vector2(scaleUp, scaleUp);
    //     float travelPercent = 0f;

    //     while (travelPercent < 1f)
    //     {
    //         travelPercent += Time.deltaTime * (duration / 3);
    //         transform.localScale = Vector2.Lerp(startScale, endScale, travelPercent);
    //         yield return new WaitForEndOfFrame();
    //     }
    // }
}
