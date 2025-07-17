using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class NumberFormatter
{
    private static readonly string[] Suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No" };

    public static string Format(double number, int decimals = 2)
    {
        if (number < 1000)
            return number.ToString("F0"); // Меньше 1000 — без сокращения

        int suffixIndex = 0;
        while (number >= 1000 && suffixIndex < Suffixes.Length - 1)
        {
            number /= 1000;
            suffixIndex++;
        }

        return number.ToString($"F{decimals}") + Suffixes[suffixIndex];
    }
}
