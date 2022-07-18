using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldShock : MonoBehaviour
{
    // Debug
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Enter");
            GetComponentInParent<EnemyController>().AttackShield();
        }
    }
}
