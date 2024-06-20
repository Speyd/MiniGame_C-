using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGame_C_.Units;

namespace MiniGame_C_
{
    internal class Map
    {
        private Unit?[,] world;
        public int Row { get; init; }
        public int Column { get; init; }

        public Map(int rows, int columns)
        {
            world = new Unit?[rows < 0 ? 1 : rows, columns < 0 ? 1 : columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    world[i, j] = null;
                }
            }

            Row = rows;
            Column = columns;
        }

        public void showMap()
        {
            Console.WriteLine(new string('-', Column + 2));

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    if (j == 0)
                        Console.Write('|');

                    if (world[i, j] != null)
                        Console.Write(world[i, j].Symbol);
                    else
                        Console.Write(' ');
                }

                Console.WriteLine('|');
            }

            Console.WriteLine(new string('¯', Column + 2));
        }
        public Unit? getElement(int row, int column)
        {
            if (row < 0 || column < 0 || row >= Row || column >= Column)
                return null;

            return world[row, column];
        }
        public void setElement(int row, int column, Unit? unit = null)
        {
            if (row < 0 || column < 0 || row >= Row || column >= Column)
                return;

            world[row, column] = unit;
        }
    }
}
