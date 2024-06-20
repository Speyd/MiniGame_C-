using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGame_C_.Units
{
    internal class Tree : Unit
    {
        public Tree(int x, int y, int hp, List<Type> vulnerabilities) : base(x, y, hp, vulnerabilities, 'T', "Tree")
        { }
        public Tree(int hp, List<Type> vulnerabilities) : this(0, 0, hp, vulnerabilities)
        { }
    }
}
