using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_.Units;

namespace MiniGame_C_.Items.Weapons
{
    internal interface ITool
    {
        bool useTool(Unit unit);
    }

}
