using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitTestDemo
{
    public class DamageCalculator
    {
        public static int CalculatorDamage(int amount, float mitigationPercent)
        {
            // return Convert.ToInt32(amount * mitigationPercent);
            return Convert.ToInt32(amount * (1.0f - mitigationPercent));
        }
    }
}
