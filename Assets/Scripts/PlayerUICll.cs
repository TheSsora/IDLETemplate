using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class PlayerUICll : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI textMoney;

    private void OnEnable()
    {
        player.OnMoneyChanged += UpdateUI;
        UpdateUI(player.GetMoney());
    }
    private void OnDisable() => player.OnMoneyChanged -= UpdateUI;

    private void UpdateUI(double money)
    {
        textMoney.text = NumberFormatter.Format(money);
    }
}
