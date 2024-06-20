using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGame_C_.Units
{
    internal class Unit
    {
        public int X { get; set; }
        public int Y { get; set; }

        protected List<Type> vulnerabilities = new List<Type>();
        public int Hp { get; set; }

        public string FullName { get; init; }
        public char Symbol { get; init; }

        public Unit(int x, int y, int hp, List<Type> _vulnerabilities, char symbol, string fullName)
        {
            X = x;
            Y = y;
            Hp = hp;
            Symbol = symbol;
            vulnerabilities = _vulnerabilities;
            FullName = fullName;
        }

        public Unit(int hp, List<Type> _vulnerabilities, char symbol, string fullName)
            : this(0, 0, hp, _vulnerabilities, symbol, fullName)
        { }

        public List<Type> getVulnerabilities()
        {
            return vulnerabilities;
        }
        public string showVulnerabilities()
        {
            StringWriter writer = new StringWriter();

            int index = 0;
            foreach (Type type in vulnerabilities)
            {
                index++;
                writer.WriteLine($"{index}. {type.ToString()}");
            }

            if (vulnerabilities.Count == 0)
                writer.WriteLine("NOUN Vulnerabilities");

            return writer.ToString();
        }
    }
}
