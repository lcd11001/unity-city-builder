using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnitTestDemo;
using UnityEngine;


public class test_character_with_inventory
{
    [Test]
    public void with_90_armor_takes_10_percent_damage()
    {
        // ARRANGE

        // Character character = new Character();
        // fixed issue when create new instance which is inheritance from MonoBehaviour
        ICharacter character = Substitute.For<ICharacter>();

        Inventory inventory = new Inventory(character);
        Item pants = new Item() { EquipSlot = EquipSlots.Legs, Armor = 40 };
        Item shield = new Item() { EquipSlot = EquipSlots.RightHand, Armor = 50 };

        // ACT
        inventory.EquipItem(pants);
        inventory.EquipItem(shield);

        // character.Inventory = inventory;
        character.Inventory.Returns(inventory);

        // ASSERT
        int finalDamage = DamageCalculator.CalculatorDamage(1000, character);
        Assert.AreEqual(100, finalDamage);
    }

    [Test]
    public void tells_character_when_an_item_is_equipped_successfully()
    {
        // ARRANGE

        // Character character = new Character();
        // fixed issue when create new instance which is inheritance from MonoBehaviour
        ICharacter character = Substitute.For<ICharacter>();

        Inventory inventory = new Inventory(character);
        Item armor = new Item() { EquipSlot = EquipSlots.Chest, Armor = 40 };

        // ACT
        inventory.EquipItem(armor);

        // ASSERT
        character.Received().OnItemEquipped(armor);
    }
}
