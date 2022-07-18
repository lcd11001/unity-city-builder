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

        public static int CalculatorDamage(int amount, ICharacter character)
        {
            int totalArmor = character.Inventory.GetTotalArmor() + (character.Level * 10);
            float mitigationPercent = totalArmor / 100f;
            return CalculatorDamage(amount, mitigationPercent);
        }
    }
}
