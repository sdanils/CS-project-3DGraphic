using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Four_WinAdd
{
    internal class FunctionOne
    {
        public static double Mean(double x, double y)
        {
            return x * x * x * x / 900 + y * y * y * y / 900 - x * x * y * y / 900 - 5;
        }
    }
}
