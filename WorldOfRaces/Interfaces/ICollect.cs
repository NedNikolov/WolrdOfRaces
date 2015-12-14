using System.Collections.Generic;
using WorldOfRaces.Items;


namespace WorldOfRaces.Interfaces
{
    public interface ICollect
    {
        IEnumerable<Item> Inventory { get; }

        void AddItemToInventory(Item item); 
    }
}
