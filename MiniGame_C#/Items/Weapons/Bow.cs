using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_.Units;

namespace MiniGame_C_.Items.Weapons
{
    internal class Bow : Weapon
    {
        public Bow(float damage, int solidity, int index, Inventory? inventory = null)
            : base(damage, solidity, index, "Bow", inventory)
        { }

        public override bool useWeapon(Unit? unit)
        {
            if (Solidity <= 0 || unit is null)
                return false;

            if (findType(unit, typeof(Bow)) == true)
            {
                unit.Hp -= (int)Math.Round(Damage);
                Solidity--;
                return unit.Hp <= 0 ? true : false;
            }
            return false;
        }

    }
}
