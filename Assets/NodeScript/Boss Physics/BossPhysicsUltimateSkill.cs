using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class BossPhysicsUltimateSkill : ActionNode
{
    public GameObject markerPrefab;
    public GameObject preMarkerPrefab;
    public int randomSet;

    public float preDuration;
    public float duration;

    public bool isHorizontalAttack;

    private float startTime;
    private bool isSummonVerticalMarker;
    private bool isSummonHorizontalMarker;

    public int markerNumber = 17;

    protected override void OnStart() {
        isSummonVerticalMarker = false;
        isSummonHorizontalMarker = false;
        startTime = Time.time;
        

        //Pre Attack
        if (isHorizontalAttack)
        {
            randomSet = Random.Range(1, 6);
            SummonHorizontalMarkerSet(randomSet, preMarkerPrefab, preDuration);
        }
        else
        {
            randomSet = Random.Range(1, 11);
            SummonVerticalMarkerSet(randomSet, preMarkerPrefab, preDuration);
        }
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        //Attack here
        if (isHorizontalAttack)
        {
            if (Time.time - startTime > preDuration && !isSummonHorizontalMarker)
            {
                Debug.Log("Summon Weapon");
                isSummonHorizontalMarker = true;
                SummonHorizontalMarkerSet(randomSet, markerPrefab, duration);
            }
        }
        else
        {
            if (Time.time - startTime > preDuration && !isSummonVerticalMarker)
            {
                isSummonVerticalMarker = true;
                SummonVerticalMarkerSet(randomSet, markerPrefab, duration);
            }

        }
        
        if (Time.time - startTime > preDuration + duration)
        {
            return State.Success;
        }
        return State.Running;
    }

    private void SummonVerticalMarkerSet(int setNumber, GameObject prefab, float duration)
    {
        switch (setNumber)
        {
            // 0 is not summon marker -- 1 is summon marker //
            case 1:
                SummonVerticalPosition(new List<int>() { 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0 }, prefab, duration);
                break;
            case 2:
                SummonVerticalPosition(new List<int>() { 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 1 }, prefab, duration);
                break;
            case 3:
                SummonVerticalPosition(new List<int>() { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 }, prefab, duration);
                break;
            case 4:
                SummonVerticalPosition(new List<int>() { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 }, prefab, duration);
                break;
            case 5:
                SummonVerticalPosition(new List<int>() { 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 }, prefab, duration);
                break;
            case 6:
                SummonVerticalPosition(new List<int>() { 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1 }, prefab, duration);
                break;
            case 7:
                SummonVerticalPosition(new List<int>() { 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1 }, prefab, duration);
                break;
            case 8:
                SummonVerticalPosition(new List<int>() { 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1 }, prefab, duration);
                break;
            case 9:
                SummonVerticalPosition(new List<int>() { 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1, 1, 0 }, prefab, duration);
                break;
            case 10:
                SummonVerticalPosition(new List<int>() { 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1 }, prefab, duration);
                break;

            default:
                break;
        }     
    }

    private void SummonHorizontalMarkerSet(int setNumber, GameObject prefab, float duration)
    {
        switch (setNumber)
        {
            case 1:
                SummonHorizontalPosition(new List<int>() { 1, 0, 1, 0, 1, 0, 1, 0}, prefab, duration);
                break;
            case 2:
                SummonHorizontalPosition(new List<int>() { 0, 1, 0, 1, 0, 1, 0, 1}, prefab, duration);
                break;
            case 3:
                SummonHorizontalPosition(new List<int>() { 0, 0, 1, 1, 0, 0, 1, 1 }, prefab, duration);
                break;
            case 4:
                SummonHorizontalPosition(new List<int>() { 1, 1, 0, 0, 1, 1, 0, 0 }, prefab, duration);
                break;
            case 5:
                SummonHorizontalPosition(new List<int>() { 1, 0, 0, 1, 1, 0, 0, 1 }, prefab, duration);
                break;

            default:
                break;

        }
    }

    private void SummonHorizontalPosition(List<int> pos, GameObject prefab, float duration)
    {
        for (int i = 0; i < markerNumber; i++)
        {
            if (pos[i] == 1)
            {
                GameObject preMarker = Instantiate(prefab, new Vector2(-11.5f, -5f + (1.5f * i) ), Quaternion.AngleAxis(-90f, Vector3.forward));
                Destroy(preMarker, duration);
                
            }
        }
        
        
    }

    private void SummonVerticalPosition(List<int> pos, GameObject prefab, float duration)
    {
        for (int i = 0; i < markerNumber; i++)
        {
            if (pos[i] == 1)
            {
                GameObject preMarker = Instantiate(prefab, new Vector2(-11.5f + (1.5f * i), -5f), Quaternion.identity);
                Destroy(preMarker, duration);

            }
        }


    }
}
