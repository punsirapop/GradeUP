using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerLoud : MonoBehaviour
{
    [SerializeField] GameObject soundAttackPrefab;
    [SerializeField] float soundAttackForce = 10f;

    Vector2[] patrolPoints = new Vector2[4];
    float speed = .15f;
    float attackDuration = 5f;

    private Vector2 playerPos;
    private Vector2 enemyPos;

    private float distanceX;
    private float distanceY;


    public void Setup(Vector2[] patrolPoints, float speed)
    {
        this.patrolPoints = patrolPoints;
        this.speed = speed;

        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            foreach (Vector2 point in patrolPoints)
            {
                Vector2 startPosition = transform.position;
                Vector2 endPosition = point;
                float travelPercent = 0f;

                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector2.Lerp(startPosition, endPosition, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        playerPos = MainGame.instance.playerController.transform.position;
        enemyPos = transform.position;

        distanceX = enemyPos.x - playerPos.x;
        distanceY = enemyPos.y - playerPos.y;
    }

    public void Attack(float duration)
    {
        attackDuration = duration;

        float angle = Mathf.Atan2(distanceY, distanceX) * Mathf.Rad2Deg - 90f;
        Quaternion enemyAngle = Quaternion.AngleAxis(angle, Vector3.forward);

        SoundAttack(enemyAngle);
    }

    private void SoundAttack( Quaternion angle)
    {
        GameObject soundAttack = Instantiate(soundAttackPrefab, enemyPos, angle);
        Rigidbody2D rbSound = soundAttack.GetComponent<Rigidbody2D>();
        rbSound.AddForce(rbSound.transform.up * -1 * soundAttackForce, ForceMode2D.Impulse);
        
        Destroy(soundAttack, attackDuration);
    }
}
