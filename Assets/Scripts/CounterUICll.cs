using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterUICll : MonoBehaviour
{
    [SerializeField] private IDLECounter counter;
    
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private TextMeshProUGUI textValue;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        counter.OnDataUpdate += UpdateUI;
        counter.OnTimeUpdate += SliderUI;
        counter.UpdateCurrentValues();
    }

    private void OnDisable()
    {
        counter.OnTimeUpdate -= SliderUI;
        counter.OnDataUpdate -= UpdateUI;
    }

    private void UpdateUI(double value, double price, float delayTime)
    {
        textPrice.text = NumberFormatter.Format(price);
        textValue.text = NumberFormatter.Format(value);
        slider.maxValue = delayTime;
    }
    private void SliderUI(float obj)
    {
        slider.value = obj;
    }
}
