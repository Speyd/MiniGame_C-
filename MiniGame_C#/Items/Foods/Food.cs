using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGame_C_.Items.Foods
{
    internal class Food : Item
    {
        public int NutritionalValue { get; init; }

        public Food(int nutritionalValue, int index, string textItem)
            : base(index, textItem)
        {
            NutritionalValue = nutritionalValue;
        }

        public override string getFullInfo()
        {
            StringWriter writer = new StringWriter();

            writer.WriteLine($"Name: {TextItem}");
            writer.WriteLine($"NutritionalValue: {NutritionalValue}");

            return writer.ToString();
        }
    }
}
