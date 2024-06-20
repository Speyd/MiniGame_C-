using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MiniGame_C_.Items
{
    internal abstract class Item
    {
        public Inventory? Keeper { get; set; }
        public int Index { get; init; }
        public string TextItem { get; init; }
        public Item(int index, string textItem, Inventory? keeper = null)
        {
            Index = index;
            TextItem = textItem;
            Keeper = keeper;
        }
        public abstract string getFullInfo();
    }
}
