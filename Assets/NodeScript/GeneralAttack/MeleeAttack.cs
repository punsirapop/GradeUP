using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MeleeAttack : ActionNode
{
    [Header("Attack Area Object")]
    public GameObject attackAreaPrefab;
    public enemySO attackOwnerStat;

    [Header("Attack Setting")]
    public float attackDuration = 1;
    public float delayAttack;
    public float extraRange;
    public float attackAngle;

    public enum AttackType
    {
        playerDirection,
        fourDirection,
        fourAngle,
        xAxis,
        yAxis
    }

    [Header("Attack Type")]
    public AttackType type;

    public bool isEnemyAttackPlayerDirection;
    public bool isEnemyAttackShortestAxis;
    public bool isEnemyAttackOnlyXAxis;
    public bool isEnemyAttackOnlyYAxis;
    public bool isEnemyAttackBothAxis;
    public bool isEnemyAttack4Direction;
    public bool isEnemyAttack4Angle;


    private bool isAlreadyAttack;  
    private float playerSignX; // if player is left or top to enemy set to 1//
    private float playerSignY; // if player is rigt or bottom to enemy set to -1//
    private float startTime;
    private float distanceX;
    private float distanceY;

    private Vector2 direction;
    private Vector2 playerPos;
    private Vector2 enemyPos;

    protected override void OnStart() {
        playerPos = MainGame.instance.playerController.transform.position;
        enemyPos = context.transform.position;

        distanceX = playerPos.x - enemyPos.x;
        distanceY = playerPos.y - enemyPos.y;

        playerSignX = (distanceX >= 0) ? 1 : -1;
        playerSignY = (distanceY >= 0) ? 1 : -1;

        direction = (playerPos - enemyPos);
        direction.Normalize();

        startTime = Time.time;  
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        if (!isAlreadyAttack) CheckAttackType();
        if (Time.time - startTime > attackDuration)
        {
            isAlreadyAttack = false;
            return State.Success;
        }
        return State.Running;
    }

    private void CheckAttackType()
    {
        if( Time.time - startTime > delayAttack)
        {
            isAlreadyAttack = true;
            if (type == AttackType.playerDirection) AttackPlayerDirection();
            else if (type == AttackType.fourDirection) Attack4Direction();
            else if (type == AttackType.fourAngle) Attack4DirectionAngle();
            else if (type == AttackType.xAxis) AttackOnlyXAxis();
            else if (type == AttackType.yAxis) AttackOnlyYAxis();
            else if (isEnemyAttackShortestAxis) AttackShortestAxis();
            else if (isEnemyAttackBothAxis) AttackBothAxis();
        }
    }

    private void AttackPlayerDirection()
    {
        Vector2 attackPos = new Vector2(enemyPos.x + (direction.x * extraRange), enemyPos.y + (direction.y * extraRange));
        Attack(attackPos, AttackDirection(distanceY, distanceX));
    }

    private void AttackShortestAxis()
    {
        // X is Shorter than Y from Player//
        if (Mathf.Abs(distanceX) < Mathf.Abs(distanceY))
        {
        }
        // Y is Shorter than X from Player//
        if (Mathf.Abs(distanceX) >= Mathf.Abs(distanceY))
        {
        }
    }
    private void AttackOnlyXAxis()
    {
        Vector2 attackPos = new(enemyPos.x + (extraRange * playerSignX), enemyPos.y);
        Attack(attackPos, AttackDirection(0, 1 * playerSignX));
    }

    private void AttackOnlyYAxis()
    {
        Vector2 attackPos = new(enemyPos.x, enemyPos.y + (extraRange * playerSignY));
        Attack(attackPos, AttackDirection(1 * playerSignY, 0));
    }

    private void AttackBothAxis()
    {
        Vector2 attackPosX = new(enemyPos.x + (extraRange * playerSignX), enemyPos.y);
        Vector2 attackPosY = new(enemyPos.x, enemyPos.y + (extraRange * playerSignY));

        Attack(attackPosX, AttackDirection(0, 1 * playerSignX));
        Attack(attackPosY, AttackDirection(1 * playerSignY, 0));
    }

    private void Attack4Direction()
    {
        Vector2 attackPosX1 = new(enemyPos.x + (extraRange * playerSignX), enemyPos.y);
        Vector2 attackPosX2 = new(enemyPos.x + (extraRange * -1 * playerSignX), enemyPos.y);
        Vector2 attackPosY1 = new(enemyPos.x, enemyPos.y + (extraRange * playerSignY));
        Vector2 attackPosY2 = new(enemyPos.x, enemyPos.y + (extraRange * -1 * playerSignY));

        Attack(attackPosX1, AttackDirection(0, 1 * playerSignX));
        Attack(attackPosX2, AttackDirection(0, -1 * playerSignX));
        Attack(attackPosY1, AttackDirection(1 * playerSignY, 0));
        Attack(attackPosY2, AttackDirection(-1 * playerSignY, 0));
    }

    private void Attack4DirectionAngle()
    {
        Vector2 attackPosX1 = new(enemyPos.x + (extraRange),      enemyPos.y + (extraRange));
        Vector2 attackPosX2 = new(enemyPos.x + (extraRange * -1), enemyPos.y + (extraRange * -1));
        Vector2 attackPosY1 = new(enemyPos.x + (extraRange),      enemyPos.y + (extraRange * -1));
        Vector2 attackPosY2 = new(enemyPos.x + (extraRange * -1), enemyPos.y + (extraRange));

        Attack(attackPosX1, AttackDirection(0, 1 * playerSignX));
        Attack(attackPosX2, AttackDirection(0, -1 * playerSignX));
        Attack(attackPosY1, AttackDirection(1 * playerSignY, 0));
        Attack(attackPosY2, AttackDirection(-1 * playerSignY, 0));
    }

    private void Attack(Vector2 attackPos, Quaternion enemyAngle)
    {
        GameObject attackArea = Instantiate(attackAreaPrefab, attackPos, enemyAngle);
        attackArea.GetComponent<AttackHandler>().attackOwner = attackOwnerStat;
        FinishAttack(attackArea);
    }

    private Quaternion AttackDirection(float yDegree, float xDegree)
    {
        float angle = Mathf.Atan2(yDegree, xDegree) * Mathf.Rad2Deg - 90f + attackAngle;
        Quaternion enemyAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        return enemyAngle;
    }

    private void FinishAttack(GameObject gameObject)
    {
         Destroy(gameObject, attackDuration);
    }
}
