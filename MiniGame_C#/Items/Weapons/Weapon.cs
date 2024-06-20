using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_.Units;

namespace MiniGame_C_.Items.Weapons
{
    internal abstract class Weapon : Item
    {
        public float Damage { get; init; }
        public int Solidity { get; set; }

        public Weapon(float damage, int solidity, int index, string textItem, Inventory? inventory = null)
            : base(index, textItem, inventory)
        {
            Damage = damage;
            Solidity = solidity;
        }

        public bool findType(Unit unit, Type type)
        {
            foreach (Type weapon in unit.getVulnerabilities())
            {
                if (type.IsAssignableFrom(weapon) || weapon.IsAssignableFrom(type))
                    return true;
            }

            return false;
        }
        public abstract bool useWeapon(Unit? unit);

        public override string getFullInfo()
        {
            StringWriter writer = new StringWriter();

            writer.WriteLine($"Name: {TextItem}");
            writer.WriteLine($"Damage: {Damage}");
            writer.WriteLine($"Solidity: {Solidity}");

            return writer.ToString();
        }
    }
}
