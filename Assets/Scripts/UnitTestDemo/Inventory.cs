using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace UnitTestDemo
{
    public class Inventory
    {
        Dictionary<EquipSlots, Item> _equippedItems = new Dictionary<EquipSlots, Item>();
        List<Item> _unequippedItems = new List<Item>();
        
        // readonly means we only assign value in constructor
        private readonly ICharacter _character;

        public Inventory(ICharacter character)
        {
            _character = character;
        }

        public void EquipItem(Item item)
        {
            if (_equippedItems.ContainsKey(item.EquipSlot))
            {
                _unequippedItems.Add(_equippedItems[item.EquipSlot]);
            }

            _equippedItems[item.EquipSlot] = item;

            _character?.OnItemEquipped(item);
        }

        public Item GetItem(EquipSlots slot)
        {
            if (_equippedItems.ContainsKey(slot))
            {
                return _equippedItems[slot];
            }

            return null;
        }

        public int GetTotalArmor()
        {
            return _equippedItems.Values.Sum(t => t.Armor);
        }
    }
}
