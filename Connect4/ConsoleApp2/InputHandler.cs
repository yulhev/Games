using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class InputHandler
    {
        public static int GetColumnInput(int cols)
        {
            int column;
            do
            {
                Console.Write("Enter column: ");
            } while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > cols);

            return column;
        }
    }
}