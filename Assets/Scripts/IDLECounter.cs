using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class IDLECounter : MonoBehaviour
{
    public Action<double, double, float> OnDataUpdate;
    public Action<float> OnTimeUpdate;
    
    [SerializeField] private bool isActive;
    
    [SerializeField] private int level;
    
    [SerializeField] private float delayTime;
    [SerializeField] private float delayTimeFactor;
    [SerializeField] private float delayTimeLimit;
    
    [SerializeField] private float baseValue;
    [SerializeField] private float basePrice;
    
    [SerializeField] private float priceFactor;
    [SerializeField] private float valueFactor;
    
    private double _currentValue;
    private double _currentPrice;
    private float _currentDelayTime;


    private float _timer;

    private void OnEnable()
    {
        UpdateCurrentValues();
        
        if(level == 0)
            return;
        
        isActive = true;
    }

    public void LoadData(int savedLevel)
    {
        //level = savedLevel;
        //UpdateCurrentValues();
    }

    public void LevelUp()
    {
        if (Player.instance.TrySpendMoney(_currentPrice))
        {
            level++;
            UpdateCurrentValues();
        }
    }

    public void UpdateCurrentValues()
    {
        if(level == 0)
        {
            _currentPrice =  basePrice;
            _currentValue =  baseValue;
            _currentDelayTime = delayTime;
            OnDataUpdate?.Invoke(_currentValue, _currentPrice,  _currentDelayTime);
            
            return;
        }

        _currentPrice =  basePrice * MathF.Pow(priceFactor, level-1);
        _currentValue =  baseValue * MathF.Pow(valueFactor, level-1);
        
        if(delayTime * MathF.Pow(delayTimeFactor, level-1) > delayTimeLimit)
            _currentDelayTime = delayTime * MathF.Pow(delayTimeFactor, level-1);
        else
            _currentDelayTime = delayTimeLimit;
        
        OnDataUpdate?.Invoke(_currentValue, _currentPrice,  _currentDelayTime);
        
        isActive = true;
    }
    private void Update()
    {
        if (!isActive)
            return;
        
        _timer += Time.deltaTime;
        OnTimeUpdate?.Invoke(_timer);
        if (_timer >= _currentDelayTime)
        {
            Player.instance.AddMoney(_currentValue);
            _timer = 0;
        }
    }
}
