using System.Collections.Generic;
using RPG_Ned.Items;


namespace WorldOfRaces.Interfaces
{
    public interface ICollect
    {
        IEnumerable<Item> Inventory { get; }

        void AddItemToInventory(Item item); 
    }
}
