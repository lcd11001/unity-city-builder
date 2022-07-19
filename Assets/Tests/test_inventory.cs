using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnitTestDemo;
using UnityEngine;


public class test_inventory
{
    [Test]
    public void only_allows_one_chest_to_be_equipped_at_a_time()
    {
        // ARRANGE
        Inventory inventory = new Inventory(null);
        Item item1 = new Item() { EquipSlot = EquipSlots.Chest };
        Item item2 = new Item() { EquipSlot = EquipSlots.Chest };

        // ACT
        inventory.EquipItem(item1);
        inventory.EquipItem(item2);

        // ASSERT
        Item equippedItem = inventory.GetItem(EquipSlots.Chest);
        Assert.AreEqual(item2, equippedItem);
    }
}
