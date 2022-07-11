using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPool : MonoBehaviour
{
    [SerializeField] float cooldownPoison = 1f;
    [SerializeField] bool isCooldown;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !isCooldown)
        {
            Debug.Log("Player Take a poison!");
            MainGame.instance.playerController.Poisonning();
            StartCoroutine(Cooldown());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().SetVisible(false, 5);
        }
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownPoison);
        isCooldown = false;
    }
}
