using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Lab_Four_WinAdd
{
    internal class CreaterFunctions
    {
        public CreaterFunctions() {}
        public Point3D[] CreatedArrayMeans(int type)
        {
            double stepMesh = 20f / 100f;
            int rows = 100;
            int columns = rows;

            Point3D[] arrayCoord = new Point3D[rows * columns];
            int indexPoint = 0;

            for (double x = -10; x <= 10; x += stepMesh)
            {
                for (double z = -10; z <= 10; z += stepMesh)
                {
                    arrayCoord[indexPoint].X = x;
                    if (type == 1)
                    {
                        arrayCoord[indexPoint].Y = FunctionOne.Mean(x, z);
                    }
                    else if (type == 2)
                    {
                        arrayCoord[indexPoint].Y = FunctionTwo.Mean(x, z);
                    }
                    else if (type == 3)
                    {
                        arrayCoord[indexPoint].Y = FunctionThree.Mean(x, z);
                    }
                    arrayCoord[indexPoint].Z = z;
                    indexPoint++;
                }
            }

            return arrayCoord;
        }

    }
}
