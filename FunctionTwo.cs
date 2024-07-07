using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Four_WinAdd
{
    internal class FunctionTwo
    {
        public static double Mean(double x, double y)
        {
            return Math.Exp(-1 * (x * x + y * y) / 8) * (Math.Sin(x * x) + Math.Cos(y * y));
        }
    }
}
