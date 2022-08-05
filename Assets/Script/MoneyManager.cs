using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    private int _currentMoney = 5;
    public int currentMoney => _currentMoney;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void GetMoney(int amount)
    {
        _currentMoney += amount;
    }
    public void UseMoney(int amount)
    {
        _currentMoney -= amount;
    }
}
