using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class CounterUICll : MonoBehaviour
{
    [SerializeField] private IDLECounter counter;
    
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private TextMeshProUGUI textValue;

    private void OnEnable() => counter.OnDataUpdate += UpdateUI;
    private void OnDisable() => counter.OnDataUpdate -= UpdateUI;

    private void UpdateUI(double value, double price)
    {
        textPrice.text = NumberFormatter.Format(price);
        textValue.text = NumberFormatter.Format(value);
    }
}
