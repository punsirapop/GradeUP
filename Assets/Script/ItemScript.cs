using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public itemSO ItemStat;
    public float HP,Atk, Atk_Speed, MoveSpeed, Art;

    public delegate void AddStatDelegate(int hp, int atk, int atk_speed, int speed);
    public AddStatDelegate AddStat;
    void Start()
    {
        this.HP = ItemStat.HP;
        this.Atk = ItemStat.Attack;
        this.Atk_Speed = ItemStat.AttackSpeed;
        this.MoveSpeed = ItemStat.Speed;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
    */
}
