using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] StatusManager statusManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> weaponList;

    void Start()
    {
        spriteRenderer.sprite = weaponList[statusManager.ActiveSubClass];
    }
}
