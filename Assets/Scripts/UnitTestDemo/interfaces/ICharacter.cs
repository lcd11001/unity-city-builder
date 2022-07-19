using System;

namespace UnitTestDemo
{
    public interface ICharacter
    {
        Inventory Inventory { get; }
        int Health { get; }
        int Level { get; }
        void OnItemEquipped (Item item);
    }
}
