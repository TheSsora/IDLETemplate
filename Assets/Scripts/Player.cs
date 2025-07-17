using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    public Action<double> OnMoneyChanged;
    
    [SerializeField] private double Money = 100;

    public double GetMoney() => Money;
    private void Awake()
    {
        instance = this;
    }
    public void AddMoney(double amount)
    {
        Money += amount;
        OnMoneyChanged?.Invoke(Money);
    }

    public bool TrySpendMoney(double amount)
    {
        if (amount <= Money)
        {
            Money -= amount;
            OnMoneyChanged?.Invoke(Money);
            return true;
        }
        return false;
    }
}
