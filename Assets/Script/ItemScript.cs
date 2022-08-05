using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemScript : MonoBehaviour
{
    [SerializeField] itemSO ItemStat;
    public int HP, Atk, Atk_Speed, MoveSpeed, Art, Price;

    //public delegate void AddStatDelegate(int hp, int atk, int atk_speed, int speed);
    //public AddStatDelegate AddStat;
    void Awake()
    {
        this.HP = (int)ItemStat.HP;
        this.Atk = (int)ItemStat.Attack;
        this.Atk_Speed = (int)ItemStat.AttackSpeed;
        this.MoveSpeed = (int)ItemStat.Speed;
        //this.Price = ItemStat.Price;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("GetITEM");
            collision.GetComponent<StatusManager>().AddFlatBonus(HP, Atk, Atk_Speed, MoveSpeed);
            //AddStat.Invoke(HP,Atk,Atk_Speed,MoveSpeed);
            Destroy(this.gameObject);
        }
    }
}
