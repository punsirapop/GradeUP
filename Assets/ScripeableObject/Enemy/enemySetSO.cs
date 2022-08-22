using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemySet")]
public class enemySetSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemySets = new List<GameObject>();
    public List<GameObject> EnemySets => enemySets;
}
