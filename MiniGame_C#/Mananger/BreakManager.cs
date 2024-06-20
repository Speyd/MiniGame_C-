using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_.Items;
using MiniGame_C_.Items.Weapons;
using MiniGame_C_.Units;

namespace MiniGame_C_.Mananger
{
    internal class BreakManager
    {
        static public void initializatorAttack(Unit? unit, ref Item? item, UnitManager unitManager)
        {
            if (item is Weapon weapon)
            {
                if (unit is not null && weapon.useWeapon(unit))
                    unitManager.deleteUnit(unit.Y, unit.X);

                if (weapon.Solidity <= 0)
                {
                    item.Keeper?.deleteItem(item.Index);
                    item = null;
                }
            }
        }
    }
}
