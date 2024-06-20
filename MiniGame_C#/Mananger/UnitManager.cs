using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_;
using MiniGame_C_.Items;
using MiniGame_C_.Units;

namespace MiniGame_C_.Mananger
{
    internal class UnitManager
    {
        private Map map;

        public UnitManager(Map _map)
        {
            map = _map;
        }
        public void addUnit(Unit unit, int row, int column)
        {
            if (row < 0 || column < 0 || row >= map.Row || column >= map.Column)
                return;
            else if (map.getElement(row, column) != null)
                return;

            unit.Y = row;
            unit.X = column;

            map.setElement(row, column, unit);
        }
        public void deleteUnit(int row, int column)
        {
            if (row < 0 || column < 0 || row > map.Row || column > map.Column)
                return;

            map.setElement(row, column);
        }
    }

}
