using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnitTestDemo;
using UnityEngine;
using UnityEngine.TestTools;

public class test_damge_calculator
{
    [Test]
    public void sets_damge_to_half_with_50_percent_mitigation()
    {
        // ACT
        int finalDamage = DamageCalculator.CalculatorDamage(10, 0.5f);

        // ASSERT
        Assert.AreEqual(5, finalDamage);
    }

    [Test]
    public void calculate_2_damge_from_10_with_80_percent_mitigation()
    {
        // ACT
        int finalDamage = DamageCalculator.CalculatorDamage(10, 0.8f);

        // ASSERT
        Assert.AreEqual(2, finalDamage);
    }
}
