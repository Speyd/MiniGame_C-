using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_.Units;

namespace MiniGame_C_.Items.Weapons
{
    internal class Pick : Weapon, ITool
    {
        public Pick(float damage, int solidity, int index, Inventory? inventory = null)
            : base(damage, solidity, index, "Pick", inventory)
        { }

        public bool useTool(Unit unit)
        {
            if (Solidity <= 0)
                return false;

            if (findType(unit, typeof(Pick)) == true)
            {
                unit.Hp -= (int)Math.Round(Damage);
                Solidity--;
                return true;
            }
            return false;
        }

        public override bool useWeapon(Unit? unit)
        {
            if (Solidity <= 0 || unit == null)
                return false;

            if (findType(unit, typeof(Pick)) is true)
            {
                unit.Hp -= (int)Math.Round(Damage);
                Solidity--;
                return unit.Hp <= 0 ? true : false;
            }
            return false;
        }
    }
}
