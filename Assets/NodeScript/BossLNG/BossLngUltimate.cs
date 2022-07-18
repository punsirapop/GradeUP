using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossLngUltimate : ActionNode
{
    public GameObject shieldShockPrefab;
    public GameObject[] alphabetPrefabs = new GameObject[26];
    // public Vector2 scaleAlphabet;
    // public Vector2 positionAlphabet;
    public float delayAttack = 2f;

    public float maxShieldHealth;

    [Header("Debug")]
    public float skillDuration = 5f;

    EnemyController enemyController;
    GameObject shieldShock;
    bool isShieldBreak = false;
    float startTime;

    bool isAttack;
    float startDelayTime;
    GameObject nowCharObj;

    System.Random rdm = new System.Random();

    protected override void OnStart() {
        enemyController = context.gameObject.GetComponent<EnemyController>();

        startTime = Time.time;

        CreateShieldShock();
        nowCharObj = SpawnCharacterStomp();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (Time.time - startDelayTime > delayAttack && nowCharObj == null)
        {
            isAttack = true;
        }

        if (isAttack)
        {
            nowCharObj = SpawnCharacterStomp();
        }

        isShieldBreak = enemyController.GetIsShieldBreak();
        Debug.Log(isShieldBreak);
        // if (Time.time - startTime > skillDuration || isShieldBreak)
        if (isShieldBreak)
        {
            Destroy(shieldShock);
            return State.Success;
        }
        return State.Running;
    }

    private void CreateShieldShock()
    {
        isShieldBreak = false;
        shieldShock = Instantiate(shieldShockPrefab, context.transform);
        enemyController.CreateShield(maxShieldHealth);

        isAttack = true;
    }

    GameObject SpawnCharacterStomp()
    {
        // int rdmIndex = rdm.Next(alphabetPrefabs.Length);
        
        // Debug
        int rdmIndex = rdm.Next(2);

        GameObject alphaPrefab = Instantiate(alphabetPrefabs[rdmIndex], blackboard.centerPosition, Quaternion.identity);
        alphaPrefab.transform.localScale = blackboard.scaleToFillMap;

        isAttack = false;
        startDelayTime = Time.time;
        Destroy(alphaPrefab, delayAttack - (delayAttack / 3));
        return alphaPrefab;
    }
}
