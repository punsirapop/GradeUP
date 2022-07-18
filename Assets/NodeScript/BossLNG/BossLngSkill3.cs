using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using System;

public class BossLngSkill3 : ActionNode
{

    public List<string> words;
    public bool isMaxChar;
    public int maxChar = 5;
    public bool isRandomChar;

    public GameObject charAlphabetPrefab;
    public float spaceCharAlphabet = 2;
    public Sprite[] alphabetSprites = new Sprite[26];

    public float bossMove = .5f;
    public float charFallForce = 10f;
    public float charFallDuration = 2f;
    float distanceX = -1;
    // public int nextCharIndex;
    public GameObject nextCharObject;

    bool isSuccess;
    string wordRandom;
    GameObject charParent;
    List<GameObject> charGameObjects;

    string alphabet = "abcdefghijklmnopqrstuvwxyz";
    System.Random rdm = new System.Random();

    bool isEnemyMove;

    protected override void OnStart() {
        isSuccess = false;
        isEnemyMove = false;

        charGameObjects = new List<GameObject>();

        AlphaFalling();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (isSuccess)
        {
            Destroy(charParent, 2f);
            return State.Success;
        }

        if (isEnemyMove)
        {
            MoveToNextChar();
            if (Mathf.Abs(distanceX) <= 0.5)
            {
                isEnemyMove = false;
                CharFalling();
                if (charGameObjects.Count != 0)
                {
                    NextCharAttacking();
                }
                else
                {
                    isSuccess = true;
                }
            }
        }
        return State.Running;
    }

    private void AlphaFalling()
    {
        CheckRandom();
        CreateWord();
        NextCharAttacking();
    }

    private void CheckRandom()
    {
        if (isRandomChar)
        {
            if (maxChar == 0)
            {
                maxChar = 5;
            }

            wordRandom = RandomChar();
        }
        else
        {
            wordRandom = RandomWord();
        }
        Debug.Log(wordRandom);
    }

    private string RandomChar()
    {
        string rdmChars = "";
        while (rdmChars.Length < maxChar)
        {
            int rdmIndex = rdm.Next(alphabet.Length);
            if (!rdmChars.Contains(alphabet[rdmIndex]))
            {
                rdmChars += alphabet[rdmIndex];
            }
        }
        return rdmChars;
    }

    private string RandomWord()
    {
        while (true)
        {
            int rdmIndex = rdm.Next(words.Count);
            if (!isMaxChar || words[rdmIndex].Length <= maxChar)
            {
                return words[rdmIndex];
            }
        }
    }

    private void CreateWord()
    {
        charParent = new GameObject("AlphaFallings");

        for (int i = 0; i < wordRandom.Length; i++)
        {
            char ch = wordRandom[i];
            GameObject charObj = Instantiate(charAlphabetPrefab, charParent.transform);

            charObj.transform.position = new Vector2(((charObj.transform.localScale.x / 2) + spaceCharAlphabet) * i, 0);
            charGameObjects.Add(charObj);
        }

        Transform enemyTrans = context.transform;
        float newPosX = enemyTrans.position.x - ((Vector2.Distance(charGameObjects[0].transform.position, charGameObjects[wordRandom.Length - 1].transform.position)) / 2);

        float newPosY = enemyTrans.position.y - ((enemyTrans.localScale.y / 2) + 0.5f + (charAlphabetPrefab.transform.localScale.y / 2));

        charParent.transform.position = new Vector2(newPosX, newPosY);
    }

    private void NextCharAttacking()
    {
        int rdmChar = rdm.Next(charGameObjects.Count);
        nextCharObject = charGameObjects[rdmChar];
        isEnemyMove = true;
    }

    private void MoveToNextChar()
    {
        distanceX = nextCharObject.transform.position.x - context.transform.position.x;
        Vector2 direction = new Vector2(Mathf.Sign(distanceX) * 1f, 0f);
        // context.physics.MovePosition(context.physics.position + bossMove * Time.fixedDeltaTime * direction);
        context.transform.position = context.physics.position + bossMove * Time.fixedDeltaTime * direction;
    }

    private void CharFalling()
    {
        Rigidbody2D rb = nextCharObject.GetComponent<Rigidbody2D>();
        rb.AddForce(rb.transform.up * -1 * charFallForce, ForceMode2D.Impulse);
        
        charGameObjects.Remove(nextCharObject);
        Destroy(nextCharObject, charFallDuration);
        isEnemyMove = true;
    }
}
