using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelingBox : MonoBehaviour
{
    [SerializeField] private int boxType, HealAmount, Cost;
    private bool isInteract = false;
    private void Start()
    {
        FindObjectOfType<Interactor>().OnUse += this.UseHealBox;
    }

    public void UseHealBox()
    {
        if (!this.isInteract)
        {
            if (boxType == 0)
            {
                UseFreeBox();
            }
            if (boxType == 1 && MoneyManager.Instance.currentMoney >= Cost)
            {
                UsePaidBox();
            }
            //Fill Waterbottle Charge
        }
        else
        {
            Debug.Log("You Used this Heal already");
        }

    }

    private void UseFreeBox()
    {
        HealthSystem.Instance.GetHeal(HealAmount);
        this.isInteract = true;
    }

    private void UsePaidBox()
    {
        MoneyManager.Instance.UseMoney(Cost);
        HealthSystem.Instance.GetHeal(HealAmount);
        this.isInteract = true;

    }
}
