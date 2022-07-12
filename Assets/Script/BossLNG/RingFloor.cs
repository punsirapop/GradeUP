using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFloor : MonoBehaviour
{
    float duration;
    [SerializeField] float scaleUp = 10f;

    public void Setup(float duration, int index)
    {
        this.duration = duration;
        if (index%2 == 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }

        StartCoroutine(ScaleUp());
    }

    IEnumerator ScaleUp()
    {
        Vector2 startScale = transform.localScale;
        Vector2 endScale = new Vector2(scaleUp, scaleUp);
        float travelPercent = 0f;

        while (travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * (duration / 3);
            transform.localScale = Vector2.Lerp(startScale, endScale, travelPercent);
            yield return new WaitForEndOfFrame();
        }
    }
}
