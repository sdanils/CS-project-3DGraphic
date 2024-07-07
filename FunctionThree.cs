using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Four_WinAdd
{
    internal class FunctionThree
    {
        public static double Mean(double x, double y)
        {
            return Math.Sqrt(x * x + y * y) + 3 * Math.Cos(Math.Sqrt(x * x + y * y)) - 3;
        }
    }
}
