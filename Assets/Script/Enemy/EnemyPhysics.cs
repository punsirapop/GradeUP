using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysics : MonoBehaviour
{
    private string status = "Waiting";

    GameObject player;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        Debug.Log("lr = " + lr);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(status == "Running")
        {
            StartCoroutine(Attack());
        }
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        // Rotate enemy
        Vector3 rotationVector = player.transform.position - transform.position;
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, .05f);

        // Check if hit something
        RaycastHit2D[] hits = Physics2D.RaycastAll(lr.transform.position, transform.rotation * new Vector2(0f, 1000f));
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.up * 1000f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                lr.SetPosition(1, Vector3.up * hit.distance);
            }
        }
        yield return new WaitForSeconds(2f);
        SetStatus("Success");
    }

    public void SetStatus(string newStatus)
    {
        status = newStatus;
    }

    public string GetStatus()
    {
        return status;
    }
}
