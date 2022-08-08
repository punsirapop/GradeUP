using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDestroy : MonoBehaviour
{
    [SerializeField] float duration;
    void Awake()
    {
        StartCoroutine(DestroySelf(duration));
    }
    private IEnumerator DestroySelf(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
