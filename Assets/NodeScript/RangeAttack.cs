using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RangeAttack : ActionNode
{
    [Header("Attack Bullet")]
    public GameObject bulletPrefab;

    [Header("Attack Setting")]
    public float attackDuration = 1;
    public float bulletDuration = 1.5f;
    public float bulletForce = 20f;

    private Vector2 direction;
    private Vector2 playerPos;
    private Vector2 enemyPos;
    private Vector2 attackPos;

    private float distanceX;
    private float distanceY;

    private float playerSignX;
    private float playerSignY;

    public bool isAttackManyDirection;
    public int maxDirection = 4;

    private float startTime;


    protected override void OnStart()
    {
        playerPos = MainGame.instance.playerController.transform.position;
        enemyPos = context.transform.position;

        distanceX = playerPos.x - enemyPos.x;
        distanceY = playerPos.y - enemyPos.y;

        playerSignX = (distanceX >= 0) ? 1 : -1;
        playerSignY = (distanceY >= 0) ? 1 : -1;

        direction = (playerPos - enemyPos);
        direction.Normalize();

        CheckAttackType();
        startTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > attackDuration)
        {
            return State.Success;
        }
        return State.Running;
    }

    private void CheckAttackType()
    {
        if (isAttackManyDirection) AttackManyDirection();
        else AttackPlayer();
    }

    private void AttackPlayer()
    {
        float angle = Mathf.Atan2(distanceY, distanceX) * Mathf.Rad2Deg - 90f;
        Quaternion enemyAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        attackPos = new Vector2(enemyPos.x, enemyPos.y);
        SpawnBullet(attackPos, enemyAngle);
    }

    private void AttackManyDirection()
    {
        for (int i = 0; i < maxDirection; i++)
        {
            Quaternion enemyAngle = Quaternion.AngleAxis(i * (360f / maxDirection), Vector3.forward);
            attackPos = new Vector2(enemyPos.x, enemyPos.y);
            SpawnBullet(attackPos, enemyAngle);
        }
    }
    private void SpawnBullet(Vector2 attackPos, Quaternion enemyAngle)
    {
        GameObject bullet = Instantiate(bulletPrefab, attackPos, enemyAngle);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(rb.transform.up * bulletForce, ForceMode2D.Impulse);
        FinishAttack(bullet);
    }

    private void FinishAttack(GameObject gameObject)
    {
        Destroy(gameObject, bulletDuration);
    }
}
