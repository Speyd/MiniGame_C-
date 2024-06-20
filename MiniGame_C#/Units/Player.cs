using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_;
using MiniGame_C_.Items;
using MiniGame_C_.Items.Foods;

namespace MiniGame_C_.Units
{
    internal class Player : Unit
    {

        private Inventory inventory = new Inventory();

        public Player(int x, int y, int hp, List<Type> vulnerabilities) : base(x, y, hp, vulnerabilities, 'P', "Player")
        { }

        public Player(int hp, List<Type> vulnerabilities) : this(0, 0, hp, vulnerabilities)
        { }

        public Inventory getInventory()
        {
            return inventory;
        }

        public void eatFood(Item? item)
        {
            if (item is not null && item is Food food)
            {
                Hp += food.NutritionalValue;
                inventory.deleteItem(item.Index);
            }
        }
    }
}
