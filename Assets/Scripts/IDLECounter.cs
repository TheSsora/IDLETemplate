using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class IDLECounter : MonoBehaviour
{
    public Action<double, double> OnDataUpdate;
    
    [SerializeField] private int level;
    [SerializeField] private float delayTime;
    
    [SerializeField] private float baseValue;
    [SerializeField] private float basePrice;
    
    [SerializeField] private float priceFactor;
    [SerializeField] private float valueFactor;
    
    private double currentValue;
    private double currentPrice;


    private float _timer;

    private void OnEnable()
    {
        _timer = delayTime;
        UpdateCurrentValues();
    }

    public void LoadData(int savedLevel)
    {
        level = savedLevel;
        UpdateCurrentValues();
    }

    public void LevelUp()
    {
        if (Player.instance.TrySpendMoney(currentPrice))
        {
            level++;
            UpdateCurrentValues();
        }
    }

    private void UpdateCurrentValues()
    {
        currentPrice =  (int)(basePrice * MathF.Pow(priceFactor, level-1));
        currentValue =  (int)(baseValue * MathF.Pow(valueFactor, level-1));
        OnDataUpdate?.Invoke(currentValue, currentPrice);
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Player.instance.AddMoney(currentValue);
            _timer = delayTime;
        }
    }
}
