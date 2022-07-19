using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnitTestDemo
{
    public class Character : MonoBehaviour, ICharacter
    {
        public Inventory Inventory { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }

        public void OnItemEquipped(Item item)
        {
            Debug.Log($"You equipped the {item} in {item.EquipSlot}");
        }
    }
}
