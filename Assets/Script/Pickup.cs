using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject Itembutton;
    

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
      if (other.CompareTag("Player"))
      {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isfull[i] ==  false)
            {                
                //item canbe add in inventory
                inventory.isfull[i] = true;
                Instantiate(Itembutton, inventory.slots[i].transform, false ); 
                Destroy(gameObject);
                break;
            }
            
        }
      }  
    }
   
    
        
    
}
