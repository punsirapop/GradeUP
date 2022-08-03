using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SubclassInfo")]
public class SubclassInfoSO : ScriptableObject
{
    [SerializeField] private string[] subclassName;
    public string getName(int index)
    {
        return subclassName[index];
    }

    [SerializeField] private string[] subclassDesc;
    public string getDesc(int index)
    {
        return subclassDesc[index];
    }

    [SerializeField] private Sprite[] subclassPic;
    public Sprite getPic(int index)
    {
        return subclassPic[index];
    }
}
