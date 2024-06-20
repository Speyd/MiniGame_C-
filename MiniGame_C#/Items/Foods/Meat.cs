using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGame_C_.Items.Foods
{
    internal class Meat : Food
    {
        public Meat(int nutritionalValue, int index)
                : base(nutritionalValue, index, "Meat")
        { }
    }
}
