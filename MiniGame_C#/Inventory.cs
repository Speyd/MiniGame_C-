using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_.Items;

namespace MiniGame_C_
{
    internal class Inventory
    {

        private List<Item> inventory = new List<Item>();

        public int findItemIndex(int index)
        {
            int itemIndex = -1;
            foreach (Item item in inventory)
            {
                itemIndex++;
                if (item.Index == index)
                    return itemIndex;
            }
            return -1;
        }

        public void addItem(Item item)
        {
            item.Keeper = this;
            inventory.Add(item);
        }
        public void deleteItem(int index)
        {
            int removeIndex = findItemIndex(index);
            if (removeIndex != -1)
                inventory.RemoveAt(removeIndex);
        }

        public void deleteItemIndex(int index)
        {
            inventory.RemoveAt(index);
        }
        public string showInventory()
        {
            StringWriter writer = new StringWriter();

            int index = 0;
            foreach (Item item in inventory)
            {
                index++;
                writer.WriteLine($"{index}.{item.TextItem} | index: {item.Index}");
            }

            return writer.ToString();
        }

        public Item? getItem(int index)
        {
            return index < inventory.Count && index >= 0 ? inventory[index] : null;
        }
        public string getFullInformationItem(int index)
        {
            if (index < 0 || index >= inventory.Count)
                return "ERROR";

            return inventory[index].getFullInfo();
        }

        public int getSize()
        {
            return inventory.Count;
        }
    }
}
