using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField] itemSO ItemStat;
    public float HP,Atk, Atk_Speed, MoveSpeed, Art;
    void Start()
    {
        this.HP = ItemStat.HP;
        this.Atk = ItemStat.Attack;
        this.Atk_Speed = ItemStat.AttackSpeed;
        this.MoveSpeed = ItemStat.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("GetITEM");
            Destroy(this.gameObject);
        }
    }
}
